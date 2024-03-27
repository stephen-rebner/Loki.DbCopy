using System.Data.SqlClient;
using Dapper;
using Loki.MsSqlDbCopy.Infrastructure.Repositories.Interfaces;

namespace Loki.MsSqlDbCopy.Infrastructure.Repositories;

public class MsSqlSchemasRepository : IMsSqlSchemasRepository
{
    public async Task<string[]> LoadSchemas(string connectionString)
    {
        await using var sqlConnection = new SqlConnection(connectionString);

        // Query to get all schemas
        var retrieveSchemasSql = @"SELECT name AS SchemaName
                    FROM sys.schemas
                    WHERE name NOT IN 
                    (
                        'dbo', 
                        'sys',
                        'db_accessadmin',
                        'db_backupoperator',
                        'db_datareader',
                        'db_datawriter',
                        'db_ddladmin',
                        'db_denydatareader',
                        'db_denydatawriter',
                        'db_owner',
                        'db_securityadmin',
                        'guest',
                        'INFORMATION_SCHEMA'
                    );";

        var sourceSchemas = await sqlConnection.QueryAsync<string>(retrieveSchemasSql);

        return sourceSchemas?.ToArray() ?? Array.Empty<string>();
    }

    public async Task CreateSchemas(string connectionString, string[] schemaNames)
    {
        await using var sqlConnection = new SqlConnection(connectionString);
        await sqlConnection.OpenAsync();

        foreach (var schemaName in schemaNames)
        {
            var createSchemaSql = $"CREATE SCHEMA [{schemaName}];";
            await sqlConnection.ExecuteAsync(createSchemaSql);
        }
    }
}