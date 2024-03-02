using CommunityToolkit.Diagnostics;
using Loki.DbCopy.Core.Commands;
using Loki.DbCopy.Core.Context;
using Loki.DbCopy.Core.DbCopyOptions;

namespace Loki.DbCopy.Core;

public abstract class DbCopier(IDbCopyContext dbCopyContext, IEnumerable<IDatabaseCopyCommand> databaseCopyCommands)
    : IDatabaseCopier
{
    public virtual void Copy(string sourceConnectionString, string destinationConnectionString)
    {
        Copy(sourceConnectionString, destinationConnectionString, new DbCopyOptions.DbCopyOptions());
    }

    public virtual void Copy(
        string sourceConnectionString, 
        string destinationConnectionString,
        DbCopyOptions.DbCopyOptions dbCopyOptions)
    {
        Guard.IsNotNullOrEmpty(sourceConnectionString);
        Guard.IsNotNullOrEmpty(destinationConnectionString);
        Guard.IsNotNull(dbCopyOptions);

        dbCopyContext.SourceConnectionString = sourceConnectionString;
        dbCopyContext.DestinationConnectionString = destinationConnectionString;
        dbCopyContext.DbCopyOptions = dbCopyOptions;

        foreach (var databaseCopyCommand in databaseCopyCommands)
        {
            databaseCopyCommand.Execute();
        }
    }
}