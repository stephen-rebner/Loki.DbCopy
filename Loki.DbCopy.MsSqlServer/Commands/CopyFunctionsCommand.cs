using Loki.DbCopy.MsSqlServer.Commands.Interfaces;
using Loki.MsSqlDbCopy.Infrastructure.Repositories.Interfaces;

namespace Loki.DbCopy.MsSqlServer.Commands;

public class CopyFunctionsCommand(IFunctionsRepository functionsRepository) : IDatabaseCopyCommand
{
    public async Task Execute()
    {
        var functions = await functionsRepository.GetFunctionsAsync();

        foreach (var function in functions)
        {
            await functionsRepository.CreateFunctionAsync(function);
        }
    }
}