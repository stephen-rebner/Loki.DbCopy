namespace Loki.MsSqlDbCopy.Infrastructure.Repositories.Interfaces;

public interface IMsSqlSchemasRepository
{
    Task<string[]> LoadSchemas();
    
    Task CreateSchemas(string[] schemaNames);
}