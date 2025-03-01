using Loki.MsSqlCopy.Common;

namespace Loki.MsSqlDbCopy.Infrastructure.Repositories.Interfaces;

public interface IViewsRepository
{
    Task<string[]> GetViewsAsync();
    
    Task SaveViewAsync(string viewSql);
}