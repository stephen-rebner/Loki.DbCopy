using System.Data.SqlClient;
using Dapper;
using Loki.MsSqlCopy.Common.Context;
using Loki.MsSqlDbCopy.Infrastructure.Repositories.Interfaces;

namespace Loki.MsSqlDbCopy.Infrastructure.Repositories;

public class FunctionsRepository(IConnectionStringContext connectionStringContext) : IFunctionsRepository
{
    public async Task<string[]> GetFunctionsAsync()
    {
        const string query = @"
            SELECT sm.definition
            FROM sys.sql_modules sm
            INNER JOIN sys.objects o ON sm.object_id = o.object_id
            WHERE o.type IN ('FN', 'IF', 'TF')";

        await using var connection = new SqlConnection(connectionStringContext.SourceConnectionString);
    
        var functions = await connection.QueryAsync<string>(query);
        return functions.ToArray();
    }

    public async Task CreateFunctionAsync(string function)
    {
        await using var connection = new SqlConnection(connectionStringContext.DestinationConnectionString);
        
        await connection.ExecuteAsync(function);
    }
}