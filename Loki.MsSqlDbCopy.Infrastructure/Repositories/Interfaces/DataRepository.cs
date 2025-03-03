using Loki.MsSqlCopy.Common;

namespace Loki.MsSqlDbCopy.Infrastructure.Repositories.Interfaces;

public interface IDataRepository
{
    Task<IEnumerable<T>> GetSourceDataAsync<T>(TableInfo tableInfo);
    
    Task SaveDestinationDataAsync<T>(IEnumerable<T> data, string tableName) where T : class;
}