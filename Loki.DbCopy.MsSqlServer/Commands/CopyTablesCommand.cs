using Loki.DbCopy.MsSqlServer.Commands.Interfaces;
using Loki.DbCopy.MsSqlServer.Context;
using Loki.MsSqlCopy.Common.Context;
using Loki.MsSqlDbCopy.Infrastructure.Repositories.Interfaces;

namespace Loki.DbCopy.MsSqlServer.Commands;

public class CopyTablesCommand(IMsSqlTablesRepository tablesRepository, IDbCopyContext dbCopyContext) : IDatabaseCopyCommand
{
    public async Task Execute()
    {
        if(!dbCopyContext.DbCopyOptions.CopyTables) return;
        
        var tableInfos = await tablesRepository.GetTablesAsync();
        
        foreach (var tableInfo in tableInfos)
        {
            try
            {
                await tablesRepository.SaveTableAsync(tableInfo);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error copying table {tableInfo.SchemaName}.{tableInfo.TableName}: {e.Message}");
            }
            
        }
    }
}