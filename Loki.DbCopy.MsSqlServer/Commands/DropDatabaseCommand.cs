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
            var databaseToDropName = new SqlConnectionStringBuilder(dbCopyContext.DestinationConnectionString)
                .InitialCatalog;
            
            var masterConnectionStringBuilder = new SqlConnectionStringBuilder(dbCopyContext.DestinationConnectionString)
            {
                // Change the Initial Catalog (Database) to 'master'
                InitialCatalog = "master"
            };
            
            var sqlBuilder = new SqlBuilder();
          
            using var sqlConnection = new SqlConnection(masterConnectionStringBuilder.ToString());
            
            var databaseName = sqlConnection.Database;
            
            var template = sqlBuilder.AddTemplate($"DROP DATABASE [{databaseToDropName}]");
            
            await sqlConnection.ExecuteAsync(template.RawSql); 
        }
    }
}