using System.Data.SqlClient;
using Dapper;
using Loki.DbCopy.Core.Commands;
using Loki.DbCopy.Core.Context;

namespace Loki.DbCopy.MsSqlServer.Commands;

/// <summary>
/// MsSqlServer command responsible for dropping the destination database if it exists.
/// </summary>
public class DropDatabaseCommand(IDbCopyContext dbCopyContext) : IDatabaseCopyCommand
{
    /// <summary>
    /// Drops the database if the DropAndRecreateDatabase option is set to true.
    /// Otherwise, this step is skipped.
    /// </summary>
    public async Task Execute()
    {
        if (dbCopyContext.DbCopyOptions.DropAndRecreateDatabase)
        {
            var sqlBuilder = new SqlBuilder();
          
            using var sqlConnection = new SqlConnection(dbCopyContext.DestinationConnectionString);
            
            sqlConnection.Open();
            
            var databaseName = sqlConnection.Database;
            
            sqlConnection.ChangeDatabase("master");
            
            var template = sqlBuilder.AddTemplate($"DROP DATABASE [{databaseName}]");
            
            await sqlConnection.ExecuteAsync(template.RawSql); 
        }
    }
}