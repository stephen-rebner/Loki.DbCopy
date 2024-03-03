using Loki.DbCopy.Core;
using Loki.DbCopy.Core.Commands;
using Loki.DbCopy.Core.Context;
using Loki.DbCopy.Core.DbCopyOptions;

namespace Loki.DbCopy.MsSqlServer;

public class MsSqlDbCopier(IDbCopyContext dbCopyContext, IEnumerable<IDatabaseCopyCommand> databaseCopyCommands)
    : DbCopier(dbCopyContext, databaseCopyCommands)
{
    public override async Task Copy(string sourceConnectionString, string destinationConnectionString)
    {
        await Copy(sourceConnectionString, destinationConnectionString, new DbCopyOptions());
    }

    public override async Task Copy(string sourceConnectionString, string destinationConnectionString, DbCopyOptions dbCopyOptions)
    {
       await base.Copy(sourceConnectionString, destinationConnectionString, dbCopyOptions);
    }
}