using Loki.DbCopy.MsSqlServer.Commands.Interfaces;
using Loki.MsSqlDbCopy.Infrastructure.Repositories.Interfaces;

namespace Loki.DbCopy.MsSqlServer.Commands;

public class CopyViewsCommand(IViewsRepository viewsRepository) : IDatabaseCopyCommand
{
    public async Task Execute()
    {
        var viewInfoCollection = await viewsRepository.GetViewsAsync();
        
        foreach (var viewInfo in viewInfoCollection)
        {
            await viewsRepository.SaveViewAsync(viewInfo);
        }
    }
}