using CommunityToolkit.Diagnostics;
using Loki.DbCopy.Core.Context;
using Loki.DbCopy.MsSqlServer.Commands.Interfaces;

namespace Loki.DbCopy.MsSqlServer;

public class MsSqlDbCopier(IDbCopyContext dbCopyContext, IEnumerable<IDatabaseCopyCommand> databaseCopyCommands) 
    : IMsSqlDbCopier
{
    public async Task CopyDatabaseStructure(string sourceConnectionString, string destinationConnectionString)
    {
        await CopyDatabaseStructure(sourceConnectionString, destinationConnectionString, new DbCopyOptions());
    }

    public async Task CopySchemas(string sourceConnectionString, string destinationConnectionString)
    {
        throw new NotImplementedException();
    }

    public async Task CopyDatabaseStructure(
        string sourceConnectionString, 
        string destinationConnectionString,
        DbCopyOptions dbCopyOptions)
    {
        Guard.IsNotNullOrEmpty(sourceConnectionString);
        Guard.IsNotNullOrEmpty(destinationConnectionString);
        Guard.IsNotNull(dbCopyOptions);

        await DropDatabaseAndRecreateDestinationDatabase(destinationConnectionString);
        await CopySchemas(sourceConnectionString, destinationConnectionString);
        
    }

    public async Task DropDatabaseAndRecreateDestinationDatabase(string connectionString)
    {
        throw new NotImplementedException();
    }
}