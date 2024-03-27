namespace Loki.MsSqlDbCopy.Infrastructure.Interfaces;

public interface IIndexesRepository
{
    Task<Index[]> LoadIndexes(string connectionString);
    
    Task CreateIndexes(string connectionString);
}