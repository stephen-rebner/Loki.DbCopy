using Loki.DbCopy.MsSqlServer.DatabaseCopyFunctions.Interfaces;
using Loki.DbCopy.MsSqlServer.Factories;

namespace Loki.DbCopy.MsSqlServer.DatabaseCopyFunctions;

public class MsSqlDbStructureCopier(IMsSqlDbCopyCommandFactory msSqlDbCopyCommandFactory)
    : IMsSqlDbStructureCopier
{
    public async Task CopyDatabaseAndSchemas()
    {
        var dropDatabaseCommand = msSqlDbCopyCommandFactory.CreateDropDatabaseCommand();
        var createDatabaseCommand = msSqlDbCopyCommandFactory.CreateCreateDatabaseCommand();
        var copySchemasCommand = msSqlDbCopyCommandFactory.CreateCopySchemasCommand();
        
        await dropDatabaseCommand.Execute();
        await createDatabaseCommand.Execute();
        await copySchemasCommand.Execute();
    }
    
    public async Task CopyDatabaseObjects()
    {
        var copyTablesCommand = msSqlDbCopyCommandFactory.CreateCopyTablesCommand();
        var copyDataCommand = msSqlDbCopyCommandFactory.CreateCopyDataCommand();
        var copyPrimaryKeysCommand = msSqlDbCopyCommandFactory.CreateCopyPrimaryKeysCommand();
        var copyForeignKeysCommand = msSqlDbCopyCommandFactory.CreateCopyForeignKeysCommand();
        var copyIndexesCommand = msSqlDbCopyCommandFactory.CreateCopyIndexesCommand();
        var copyViewsCommand = msSqlDbCopyCommandFactory.CreateCopyViewsCommand();
        var copyStoredProceduresCommand = msSqlDbCopyCommandFactory.CreateCopyStoredProceduresCommand();
        var copyFunctionsCommand = msSqlDbCopyCommandFactory.CreateCopyFunctionsCommand();
        var copyTriggersCommand = msSqlDbCopyCommandFactory.CreateCopyTriggersCommand();
        
        var tasks = new Task[]
        {
            copyTablesCommand.Execute(),
            copyDataCommand.Execute(),
            copyPrimaryKeysCommand.Execute(),
            copyForeignKeysCommand.Execute(),
            copyIndexesCommand.Execute(),
            copyViewsCommand.Execute(),
            copyStoredProceduresCommand.Execute(),
            copyFunctionsCommand.Execute(),
            copyTriggersCommand.Execute()
        };
        
        await Task.WhenAll(tasks);
    }
}