using Loki.DbCopy.Core;
using Loki.DbCopy.Core.Commands;
using Loki.DbCopy.Core.Context;
using Loki.DbCopy.Core.DbCopyOptions;

namespace Loki.DbCopy.MsSqlServer;

public class MsSqlDbCopier(IDbCopyContext dbCopyContext, IList<IDatabaseCopyCommand> databaseCopyCommands)
    : DbCopier(dbCopyContext, databaseCopyCommands)
{
    public override void Copy(string sourceConnectionString, string destinationConnectionString)
    {
        Copy(sourceConnectionString, destinationConnectionString, new DbCopyOptions());
    }

    public override void Copy(string sourceConnectionString, string destinationConnectionString, DbCopyOptions dbCopyOptions)
    {
       base.Copy(sourceConnectionString, destinationConnectionString, dbCopyOptions);
    }
}