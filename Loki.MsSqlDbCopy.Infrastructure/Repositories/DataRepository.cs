using System.Data.SqlClient;
using System.Text;
using Dapper;
using Loki.BulkDataProcessor;
using Loki.BulkDataProcessor.InternalDbOperations.Interfaces;
using Loki.MsSqlCopy.Common;
using Loki.MsSqlCopy.Common.Context;
using Loki.MsSqlDbCopy.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Loki.MsSqlDbCopy.Infrastructure.Repositories;

public class DataRepository(IConnectionStringContext connectionStringContext, IBulkProcessor bulkProcessor) : IDataRepository
{
    public async Task<IEnumerable<T>> GetSourceDataAsync<T>(TableInfo tableInfo)
    {
        try
        {
            var stringBuilder = new StringBuilder();
        
            stringBuilder.Append("SELECT ");
        
            var columns = tableInfo.Columns.ToArray();
        
            for(var i = 0; i < columns.Length; i++)
            {
                var column = columns[i];

                stringBuilder.Append($"[{column.ColumnName}]");
            
                if(i < columns.Length - 1)
                {
                    stringBuilder.Append(", ");
                }
            }
        
            stringBuilder.Append($" FROM [{tableInfo.SchemaName}].[{tableInfo.TableName}]");
        
            await using var sqlConnection = new SqlConnection(connectionStringContext.SourceConnectionString);
        
            return await sqlConnection.QueryAsync<T>(stringBuilder.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task SaveDestinationDataAsync<T>(IEnumerable<T> data, string tableName) where T : class
    {
        bulkProcessor.WithConnectionString(connectionStringContext.DestinationConnectionString);
        bulkProcessor.BatchSize = 5000;

        await bulkProcessor.SaveAsync(data, tableName);
    }
}