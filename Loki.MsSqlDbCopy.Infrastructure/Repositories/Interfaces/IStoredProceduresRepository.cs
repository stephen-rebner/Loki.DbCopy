namespace Loki.MsSqlDbCopy.Infrastructure.Repositories.Interfaces;

public interface IStoredProceduresRepository
{
    Task<string[]> GetStoredProceduresAsync();
    
    Task SaveStoredProceduresAsync(string storedProcedureSql);
}