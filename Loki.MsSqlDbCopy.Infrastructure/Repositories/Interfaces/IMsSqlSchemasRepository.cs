namespace Loki.MsSqlDbCopy.Infrastructure.Repositories.Interfaces;

public interface IMsSqlSchemasRepository
{
    Task<string[]> LoadSchemas(string connectionString);
    
    Task CreateSchemas(string connectionString, string[] schemaNames);
}