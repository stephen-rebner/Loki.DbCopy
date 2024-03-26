using Loki.DbCopy.MsSqlServer.Commands.Interfaces;

namespace Loki.DbCopy.MsSqlServer.Factories;

public interface IMsSqlDbCopyCommandFactory
{
    IDatabaseCopyCommand CreateCopySchemasCommand();
    
    IDatabaseCopyCommand CreateDropDatabaseCommand();
    
    IDatabaseCopyCommand CreateCreateDatabaseCommand();
}