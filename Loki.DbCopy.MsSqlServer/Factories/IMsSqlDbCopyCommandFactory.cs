using Loki.DbCopy.MsSqlServer.Commands.Interfaces;

namespace Loki.DbCopy.MsSqlServer.Factories;

public interface IMsSqlDbCopyCommandFactory
{
    IDatabaseCopyCommand CreateCopySchemasCommand();
    
    IDatabaseCopyCommand CreateDropDatabaseCommand();
    
    IDatabaseCopyCommand CreateCreateDatabaseCommand();
    
    IDatabaseCopyCommand CreateCopyTablesCommand();
    
    IDatabaseCopyCommand CreateCopyDataCommand();
    
    IDatabaseCopyCommand CreateCopyPrimaryKeysCommand();
    
    IDatabaseCopyCommand CreateCopyForeignKeysCommand();
    
    IDatabaseCopyCommand CreateCopyIndexesCommand();
    
    IDatabaseCopyCommand CreateCopyViewsCommand();
    
    IDatabaseCopyCommand CreateCopyStoredProceduresCommand();
    
    IDatabaseCopyCommand CreateCopyFunctionsCommand();
    
    IDatabaseCopyCommand CreateCopyTriggersCommand();
    
}