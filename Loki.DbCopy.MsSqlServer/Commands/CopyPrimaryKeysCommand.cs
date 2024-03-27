using Loki.DbCopy.MsSqlServer.Commands.Interfaces;

namespace Loki.DbCopy.MsSqlServer.Commands;

public class CopyPrimaryKeysCommand : IDatabaseCopyCommand
{
    public async Task Execute()
    {
        throw new NotImplementedException();

        // var primaryKeys = await indexesRepository.LoadPrimaryKeys(dbCopyContext.SourceConnectionString);
        //
        // foreach (var primaryKey in primaryKeys)
        // {
        //     var createPrimaryKeyCommand = databaseCopyCommandFactory.CreateCreatePrimaryKeyCommand(primaryKey);
        //     await createPrimaryKeyCommand.Execute();
        // }
    }
}