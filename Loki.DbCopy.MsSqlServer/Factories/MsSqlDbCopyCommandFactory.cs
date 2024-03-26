using Loki.DbCopy.MsSqlServer.Commands;
using Loki.DbCopy.MsSqlServer.Commands.Interfaces;

namespace Loki.DbCopy.MsSqlServer.Factories;

internal class MsSqlDbCopyCommandFactory(IEnumerable<IDatabaseCopyCommand?> databaseCopyCommands)
    : IMsSqlDbCopyCommandFactory
{
    public IDatabaseCopyCommand CreateCopySchemasCommand()
    {
        return databaseCopyCommands
            .FirstOrDefault(databaseCopyCommand => databaseCopyCommand!.GetType() == typeof(CopySchemasCommand))!;
    }
    
    public IDatabaseCopyCommand CreateDropDatabaseCommand()
    {
        return databaseCopyCommands
            .FirstOrDefault(databaseCopyCommand => databaseCopyCommand!.GetType() == typeof(DropDatabaseCommand))!;
    }
    
    public IDatabaseCopyCommand CreateCreateDatabaseCommand()
    {
        return databaseCopyCommands
            .FirstOrDefault(databaseCopyCommand => databaseCopyCommand!.GetType() == typeof(CreateDatabaseCommand))!;
    }
}