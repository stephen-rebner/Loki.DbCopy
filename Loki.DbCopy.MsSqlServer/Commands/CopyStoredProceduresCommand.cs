using Loki.DbCopy.MsSqlServer.Commands.Interfaces;
using Loki.MsSqlDbCopy.Infrastructure.Repositories.Interfaces;

namespace Loki.DbCopy.MsSqlServer.Commands;

public class CopyStoredProceduresCommand(IStoredProceduresRepository storedProceduresRepository) : IDatabaseCopyCommand
{
    public async Task Execute()
    {
        var collectionOfSprocsSql = await storedProceduresRepository.GetStoredProceduresAsync();
        
        foreach (var storedProcedureSql in collectionOfSprocsSql)
        {
            await storedProceduresRepository.SaveStoredProceduresAsync(storedProcedureSql);
        }
    }
}