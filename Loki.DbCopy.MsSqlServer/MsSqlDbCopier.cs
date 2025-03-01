using System.Data.SqlClient;
using Loki.DbCopy.MsSqlServer.Context;
using Loki.DbCopy.MsSqlServer.DatabaseCopyFunctions.Interfaces;
using Loki.DbCopy.MsSqlServer.Factories;
using Loki.MsSqlCopy.Common.Context;

namespace Loki.DbCopy.MsSqlServer;

public class MsSqlDbCopier(
    IDbCopyContext dbCopyContext, 
    IConnectionStringContext connectionStringContext,
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
            connectionStringContext.SourceConnectionString = sourceConnectionString.ToString();
            connectionStringContext.DestinationConnectionString = destinationConnectionString.ToString();
            dbCopyContext.DbCopyOptions = dbCopyOptions;
        
      
            var dropDatabaseCommand = msSqlDbCopyCommandFactory.CreateDropDatabaseCommand();
            var createDatabaseCommand = msSqlDbCopyCommandFactory.CreateCreateDatabaseCommand();
            var copySchemasCommand = msSqlDbCopyCommandFactory.CreateCopySchemasCommand();
            var copyTablesCommand = msSqlDbCopyCommandFactory.CreateCopyTablesCommand();
            var copyStoredProceduresCommand = msSqlDbCopyCommandFactory.CreateCopyStoredProceduresCommand();
            var copyViewsCommand = msSqlDbCopyCommandFactory.CreateCopyViewsCommand();
        
            await dropDatabaseCommand.Execute();
            await createDatabaseCommand.Execute();
            await copySchemasCommand.Execute();
            await copyTablesCommand.Execute();
            await copyStoredProceduresCommand.Execute();
            await copyViewsCommand.Execute();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}