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

    public IDatabaseCopyCommand CreateCopyTablesCommand()
    {
        return databaseCopyCommands
            .FirstOrDefault(databaseCopyCommand => databaseCopyCommand!.GetType() == typeof(CopyTablesCommand))!;
    }

    public IDatabaseCopyCommand CreateCopyDataCommand()
    {
        throw new NotImplementedException();
    }

    public IDatabaseCopyCommand CreateCopyPrimaryKeysCommand()
    {
        return databaseCopyCommands
            .FirstOrDefault(databaseCopyCommand => databaseCopyCommand!.GetType() == typeof(CopyPrimaryKeysCommand))!;
    }

    public IDatabaseCopyCommand CreateCopyForeignKeysCommand()
    {
        return databaseCopyCommands
            .FirstOrDefault(databaseCopyCommand => databaseCopyCommand!.GetType() == typeof(CopyForeignKeysCommand))!;
    }

    public IDatabaseCopyCommand CreateCopyIndexesCommand()
    {
        return databaseCopyCommands
            .FirstOrDefault(databaseCopyCommand => databaseCopyCommand!.GetType() == typeof(CopyIndexesCommand))!;
    }

    public IDatabaseCopyCommand CreateCopyViewsCommand()
    {
        return databaseCopyCommands
            .FirstOrDefault(databaseCopyCommand => databaseCopyCommand!.GetType() == typeof(CopyViewsCommand))!;
    }

    public IDatabaseCopyCommand CreateCopyStoredProceduresCommand()
    {
        return databaseCopyCommands
            .FirstOrDefault(databaseCopyCommand => databaseCopyCommand!.GetType() == typeof(CopyStoredProceduresCommand))!;
    }

    public IDatabaseCopyCommand CreateCopyFunctionsCommand()
    {
        return databaseCopyCommands
            .FirstOrDefault(databaseCopyCommand => databaseCopyCommand!.GetType() == typeof(CopyFunctionsCommand))!;
    }

    public IDatabaseCopyCommand CreateCopyTriggersCommand()
    {
        return databaseCopyCommands.FirstOrDefault(databaseCopyCommand => databaseCopyCommand!.GetType() == typeof(CopyTriggersCommand))!;
    }
}