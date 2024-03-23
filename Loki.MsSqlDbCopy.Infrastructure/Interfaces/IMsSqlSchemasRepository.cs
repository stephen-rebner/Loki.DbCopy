namespace Loki.MsSqlDbCopy.Infrastructure.Interfaces;

public interface IMsSqlSchemasRepository
{
    Task<string[]> LoadSchemas(string connectionString);
    
    Task CreateSchemas(string connectionString, string[] schemaNames);
}