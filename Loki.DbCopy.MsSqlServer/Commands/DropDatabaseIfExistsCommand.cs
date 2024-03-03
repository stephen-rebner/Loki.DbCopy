using System.Data.SqlClient;
using Dapper;
using Loki.DbCopy.Core.Commands;
using Loki.DbCopy.Core.Context;

namespace Loki.DbCopy.MsSqlServer.Commands;

public class DropDatabaseIfExistsCommand(IDbCopyContext dbCopyContext) : IDatabaseCopyCommand
{
    public async Task Execute()
    {
        if (dbCopyContext.DbCopyOptions.DropDatabaseIfExists)
        {
            var sqlBuilder = new SqlBuilder();
            var template = sqlBuilder.AddTemplate(
                    "IF EXISTS(SELECT * FROM sys.databases WHERE name = @databaseName) DROP DATABASE /**where**/");

            using var sqlConnection = new SqlConnection(dbCopyContext.DestinationConnectionString);

            sqlBuilder.Where("databaseName = @databaseName", new { sqlConnection.Database });

            await sqlConnection.ExecuteAsync(template.RawSql, template.Parameters);
        }
    }
}