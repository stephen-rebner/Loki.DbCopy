using Loki.MsSqlCopy.Common;

namespace Loki.MsSqlDbCopy.Infrastructure.Repositories.Interfaces;

public interface IMsSqlTablesRepository
{
    Task<TableInfo[]> GetTablesAsync();
    
    Task SaveTableAsync(TableInfo tableInfo);
}