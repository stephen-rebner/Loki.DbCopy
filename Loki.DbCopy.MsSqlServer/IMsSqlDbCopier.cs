namespace Loki.DbCopy.MsSqlServer;

public interface IMsSqlDbCopier
{
    Task CopyDatabaseStructure(string sourceConnectionString, string destinationConnectionString);
    
    Task CopySchemas(string sourceConnectionString, string destinationConnectionString);


    Task CopyDatabaseStructure(string sourceConnectionString, string destinationConnectionString, DbCopyOptions dbCopyOptions);

    Task DropDatabaseAndRecreateDestinationDatabase(string connectionString);
}