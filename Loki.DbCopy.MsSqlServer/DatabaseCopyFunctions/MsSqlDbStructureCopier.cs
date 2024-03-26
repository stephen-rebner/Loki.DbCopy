using Loki.DbCopy.MsSqlServer.DatabaseCopyFunctions.Interfaces;
using Loki.DbCopy.MsSqlServer.Factories;

namespace Loki.DbCopy.MsSqlServer.DatabaseCopyFunctions;

public class MsSqlDbStructureCopier(IMsSqlDbCopyCommandFactory msSqlDbCopyCommandFactory)
    : IMsSqlDbStructureCopier
{
    public async Task CopyDatabaseStructure()
    {
        var dropDatabaseCommand = msSqlDbCopyCommandFactory.CreateDropDatabaseCommand();
        var createDatabaseCommand = msSqlDbCopyCommandFactory.CreateCreateDatabaseCommand();
        var copySchemasCommand = msSqlDbCopyCommandFactory.CreateCopySchemasCommand();
        
        await dropDatabaseCommand.Execute();
        await createDatabaseCommand.Execute();
        await copySchemasCommand.Execute();
    }
}