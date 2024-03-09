using CommunityToolkit.Diagnostics;
using Loki.DbCopy.Core.Commands;
using Loki.DbCopy.Core.Context;

namespace Loki.DbCopy.Core;

public abstract class DbCopier(IDbCopyContext dbCopyContext, IEnumerable<IDatabaseCopyCommand> databaseCopyCommands)
    : IDatabaseCopier
{
    public virtual async Task Copy(string sourceConnectionString, string destinationConnectionString)
    {
        await Copy(sourceConnectionString, destinationConnectionString, new DbCopyOptions.DbCopyOptions());
    }

    public virtual async Task Copy(
        string sourceConnectionString, 
        string destinationConnectionString,
        DbCopyOptions.DbCopyOptions dbCopyOptions)
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