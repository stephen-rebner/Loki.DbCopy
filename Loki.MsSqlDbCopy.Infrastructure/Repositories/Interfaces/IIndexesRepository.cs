namespace Loki.MsSqlDbCopy.Infrastructure.Repositories.Interfaces;

public interface IIndexesRepository
{
    Task<Index[]> LoadIndexes(string connectionString);
    
    Task CreateIndexes(string connectionString);
}