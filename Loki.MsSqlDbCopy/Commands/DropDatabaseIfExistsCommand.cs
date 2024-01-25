using System.Data.SqlClient;
using Dapper;
using Loki.DbCopy.Commands.Interfaces;

namespace Loki.DbCopy.Commands;

public class DropDatabaseIfExistsCommand : IDatabaseCopyCommand
{
    private readonly MsSqlDbCopier.IDbCopyContext _dbCopyContext;

    public DropDatabaseIfExistsCommand(MsSqlDbCopier.IDbCopyContext dbCopyContext)
    {
        _dbCopyContext = dbCopyContext;
    }

    public async Task Copy()
    {
        if (_dbCopyContext.DbCopyOptions.DropDatabaseIfExists)
        {
            var sqlBuilder = new SqlBuilder();
            var template = sqlBuilder.AddTemplate("IF EXISTS(SELECT * FROM sys.databases WHERE name = @databaseName) DROP DATABASE /**where**/");
            
            using var sqlConnection = new SqlConnection(_dbCopyContext.DestinationConnectionString);
            
            sqlBuilder.Where("databaseName = @databaseName", new { sqlConnection.Database });

            await sqlConnection.ExecuteAsync(template.RawSql, template.Parameters);
        }
    }
}