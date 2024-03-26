using CommunityToolkit.Diagnostics;
using Loki.DbCopy.Core;
using Loki.DbCopy.Core.Context;
using Loki.DbCopy.MsSqlServer.Commands.Interfaces;

namespace Loki.DbCopy.MsSqlServer;

public class MsSqlDbCopier(IDbCopyContext dbCopyContext, IEnumerable<IDatabaseCopyCommand> databaseCopyCommands) 
    : IMsSqlDbCopier
{
    public async Task Copy(string sourceConnectionString, string destinationConnectionString)
    {
        await Copy(sourceConnectionString, destinationConnectionString, new DbCopyOptions());
    }

    public virtual async Task Copy(
        string sourceConnectionString, 
        string destinationConnectionString,
        DbCopyOptions dbCopyOptions)
    {
        Guard.IsNotNullOrEmpty(sourceConnectionString);
        Guard.IsNotNullOrEmpty(destinationConnectionString);
        Guard.IsNotNull(dbCopyOptions);

        dbCopyContext.SetSourceConnectionString(sourceConnectionString);
        dbCopyContext.SetDestinationConnectionString(destinationConnectionString);
        dbCopyContext.SetDbCopyOptions(dbCopyOptions);

        foreach (var databaseCopyCommand in databaseCopyCommands)
        {
            await databaseCopyCommand.Execute();
        }
    }
}