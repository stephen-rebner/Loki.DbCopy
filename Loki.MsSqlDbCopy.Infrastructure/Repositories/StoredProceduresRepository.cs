using System.Data.SqlClient;
using Dapper;
using Loki.MsSqlCopy.Common.Context;
using Loki.MsSqlDbCopy.Infrastructure.Repositories.Interfaces;

namespace Loki.MsSqlDbCopy.Infrastructure.Repositories;

public class StoredProceduresRepository(IConnectionStringContext connectionStringContext) : IStoredProceduresRepository
{
    public async Task<string[]> GetStoredProceduresAsync()
    {
        var sql = @"SELECT 
                        m.definition AS ProcedureDefinition
                    FROM 
                        sys.procedures p
                    JOIN 
                        sys.sql_modules m ON p.object_id = m.object_id
                    WHERE 
                        p.type = 'P'
                    ORDER BY 
                        p.name;";
        
        var sqlConnection = new SqlConnection(connectionStringContext.SourceConnectionString);

        var storedProcedures = await sqlConnection.QueryAsync<string>(sql);
            
        return storedProcedures.ToArray();
    }

    public async Task SaveStoredProceduresAsync(string storedProcedureSql)
    {
        var sqlConnection = new SqlConnection(connectionStringContext.DestinationConnectionString);
        
        await sqlConnection.ExecuteAsync(storedProcedureSql);
    }
}