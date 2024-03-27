using CommunityToolkit.Diagnostics;
using Loki.DbCopy.Core.Context;
using Loki.DbCopy.MsSqlServer.DatabaseCopyFunctions.Interfaces;

namespace Loki.DbCopy.MsSqlServer;

public class MsSqlDbCopier(
    IDbCopyContext dbCopyContext, 
    IMsSqlDbStructureCopier msSqlDbStructureCopier, 
    IMsSqlDbDataCopier msSqlDbDataCopier) : IMsSqlDbCopier
{
    public async Task CopyDatabaseStructure(string sourceConnectionString, string destinationConnectionString)
    {
        await CopyDatabaseStructure(sourceConnectionString, destinationConnectionString, new DbCopyOptions());
    }
    
    public async Task CopyDatabaseStructure(
        string sourceConnectionString, 
        string destinationConnectionString,
        DbCopyOptions dbCopyOptions)
    {
        ValidateUserInput(sourceConnectionString, destinationConnectionString, dbCopyOptions);
        
        dbCopyContext.SetSourceConnectionString(sourceConnectionString);
        dbCopyContext.SetDestinationConnectionString(destinationConnectionString);
        dbCopyContext.SetDbCopyOptions(dbCopyOptions);
        
        await msSqlDbStructureCopier.CopyDatabaseAndSchemas();
        await msSqlDbStructureCopier.CopyDatabaseObjects();

    }
    
    public async Task CopyDatabaseData(string sourceConnectionString, string destinationConnectionString)
    {
        await CopyDatabaseData(sourceConnectionString, destinationConnectionString, new DbCopyOptions());
    }
    
    public async Task CopyDatabaseData(
        string sourceConnectionString, 
        string destinationConnectionString,
        DbCopyOptions dbCopyOptions)
    {
        ValidateUserInput(sourceConnectionString, destinationConnectionString, dbCopyOptions);
        
        dbCopyContext.SetSourceConnectionString(sourceConnectionString);
        dbCopyContext.SetDestinationConnectionString(destinationConnectionString);
        dbCopyContext.SetDbCopyOptions(dbCopyOptions);

        // todo: disable primary keys, foreign keys, indexes, triggers, etc.
        
        await msSqlDbDataCopier.CopyDatabaseData(); 
    }
    
    public async Task CopyDatabase(string sourceConnectionString, string destinationConnectionString)
    {
        await CopyDatabase(sourceConnectionString, destinationConnectionString, new DbCopyOptions());
    }
    
    public async Task CopyDatabase(
        string sourceConnectionString, 
        string destinationConnectionString,
        DbCopyOptions dbCopyOptions)
    {
        ValidateUserInput(sourceConnectionString, destinationConnectionString, dbCopyOptions);

        dbCopyContext.SetSourceConnectionString(sourceConnectionString);
        dbCopyContext.SetDestinationConnectionString(destinationConnectionString);
        dbCopyContext.SetDbCopyOptions(dbCopyOptions);

        await msSqlDbStructureCopier.CopyDatabaseAndSchemas();
        await msSqlDbDataCopier.CopyDatabaseData();
        await msSqlDbStructureCopier.CopyDatabaseObjects();
    }

    private static void ValidateUserInput(string sourceConnectionString, string destinationConnectionString,
        DbCopyOptions dbCopyOptions)
    {
        Guard.IsNotNullOrEmpty(sourceConnectionString);
        Guard.IsNotNullOrEmpty(destinationConnectionString);
        Guard.IsNotNull(dbCopyOptions);
    }
}