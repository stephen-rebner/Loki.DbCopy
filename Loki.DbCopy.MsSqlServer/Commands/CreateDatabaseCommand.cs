using System.Data.SqlClient;
using Dapper;
using Loki.DbCopy.MsSqlServer.Commands.Interfaces;
using Loki.DbCopy.MsSqlServer.Context;
using Loki.MsSqlCopy.Common.Context;

namespace Loki.DbCopy.MsSqlServer.Commands;

/// <summary>
/// MsSqlServer command responsible for dropping the destination database if it exists.
/// </summary>
internal class CreateDatabaseCommand(IDbCopyContext dbCopyContext, IConnectionStringContext connectionStringContext) : IDatabaseCopyCommand
{
    /// <summary>
    /// Create the database if the DropAndRecreateDatabase option is set to true.
    /// Otherwise, this step is skipped.
    /// </summary>
    public async Task Execute()
    {
        if (dbCopyContext.DbCopyOptions.DropAndRecreateDatabase)
        {
            var databaseToCreateName = new SqlConnectionStringBuilder(connectionStringContext.DestinationConnectionString)
                                                .InitialCatalog;
            
            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionStringContext.DestinationConnectionString)
            {
                // Change the Initial Catalog (Database) to 'master'
                InitialCatalog = "master"
            };
            
            var sqlBuilder = new SqlBuilder();
          
            using var sqlConnection = new SqlConnection(sqlConnectionStringBuilder.ToString());
            
            var template = sqlBuilder.AddTemplate($"CREATE DATABASE [{databaseToCreateName}]");
            
            await sqlConnection.ExecuteAsync(template.RawSql); 
        }
    }
}