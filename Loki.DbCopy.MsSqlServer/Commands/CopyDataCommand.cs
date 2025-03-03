using Loki.DbCopy.MsSqlServer.Commands.Interfaces;
using Loki.MsSqlDbCopy.Infrastructure.Repositories.Interfaces;

namespace Loki.DbCopy.MsSqlServer.Commands;

public class CopyDataCommand(IMsSqlTablesRepository tablesRepository, IDataRepository dataRepository) : IDatabaseCopyCommand
{
    public async Task Execute()
    {
        var tables = await tablesRepository.GetTablesAsync();

        var getTableDataTasks = new Dictionary<string, Task<IEnumerable<object>>>();

        foreach (var table in tables)
        {
            var schemaTableName = $"[{table.SchemaName}].[{table.TableName}]";
            var getTableDataTask = dataRepository.GetSourceDataAsync<object>(table);
            getTableDataTasks.Add(schemaTableName, getTableDataTask);
        }

        await Task.WhenAll(getTableDataTasks.Values);

        var saveTableDataTasks = new List<Task>();

        foreach (var kvp in getTableDataTasks)
        {
            var schemaTableName = kvp.Key;
            var tableData = await kvp.Value;
            var saveTableDataTask = dataRepository.SaveDestinationDataAsync(tableData, schemaTableName);
            saveTableDataTasks.Add(saveTableDataTask);
        }

        await Task.WhenAll(saveTableDataTasks);
    }
}