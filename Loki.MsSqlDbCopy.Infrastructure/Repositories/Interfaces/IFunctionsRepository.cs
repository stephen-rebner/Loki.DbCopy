namespace Loki.MsSqlDbCopy.Infrastructure.Repositories.Interfaces;

public interface IFunctionsRepository
{
    Task<string[]> GetFunctionsAsync();
    
    Task CreateFunctionAsync(string function);
}