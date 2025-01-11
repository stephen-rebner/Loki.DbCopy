 using Loki.DbCopy.Core.Context;
using Loki.DbCopy.MsSqlServer.Commands.Interfaces;
using Loki.MsSqlDbCopy.Infrastructure.Repositories.Interfaces;

namespace Loki.DbCopy.MsSqlServer.Commands;

public class CopyTablesCommand(IMsSqlTablesRepository tablesRepository, IDbCopyContext dbCopyContext) : IDatabaseCopyCommand
{
    public async Task Execute()
    {
        if(!dbCopyContext.DbCopyOptions.CopyTables) return;
        
        var tableInfos = await tablesRepository.GetTablesAsync(dbCopyContext.SourceConnectionString);
        
        foreach (var tableInfo in tableInfos)
        {
            try
            {
                await tablesRepository.SaveTableAsync(dbCopyContext.DestinationConnectionString, tableInfo);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error copying table {tableInfo.SchemaName}.{tableInfo.TableName}: {e.Message}");
            }
            
        }
    }
}