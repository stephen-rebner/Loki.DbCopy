using System.Data.SqlClient;
using Loki.DbCopy.Core.Context;
using Loki.DbCopy.MsSqlServer.DatabaseCopyFunctions.Interfaces;
using Loki.DbCopy.MsSqlServer.Factories;

namespace Loki.DbCopy.MsSqlServer;

public class MsSqlDbCopier(
    IDbCopyContext dbCopyContext, 
    IMsSqlDbCopyCommandFactory msSqlDbCopyCommandFactory, 
    IMsSqlDbDataCopier msSqlDbDataCopier) : IMsSqlDbCopier
{
    
    public async Task Copy(SqlConnectionStringBuilder sourceConnectionString, SqlConnectionStringBuilder destinationConnectionString)
    {
        await Copy(sourceConnectionString, destinationConnectionString, new DbCopyOptions());
    }
    
    public async Task Copy(
        SqlConnectionStringBuilder sourceConnectionString, 
        SqlConnectionStringBuilder destinationConnectionString,
        DbCopyOptions dbCopyOptions)
    {
        try
        {
            dbCopyContext.SourceConnectionString = sourceConnectionString.ToString();
            dbCopyContext.DestinationConnectionString = destinationConnectionString.ToString();
            dbCopyContext.DbCopyOptions = dbCopyOptions;
        
      
            var dropDatabaseCommand = msSqlDbCopyCommandFactory.CreateDropDatabaseCommand();
            var createDatabaseCommand = msSqlDbCopyCommandFactory.CreateCreateDatabaseCommand();
            var copySchemasCommand = msSqlDbCopyCommandFactory.CreateCopySchemasCommand();
            var copyTablesCommand = msSqlDbCopyCommandFactory.CreateCopyTablesCommand();
        
            await dropDatabaseCommand.Execute();
            await createDatabaseCommand.Execute();
            await copySchemasCommand.Execute();
            await copyTablesCommand.Execute();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}