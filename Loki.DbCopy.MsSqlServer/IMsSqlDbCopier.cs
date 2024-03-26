namespace Loki.DbCopy.MsSqlServer;

public interface IMsSqlDbCopier
{
    Task CopyDatabaseStructure(string sourceConnectionString, string destinationConnectionString);
    
    Task CopyDatabaseStructure(string sourceConnectionString, string destinationConnectionString, DbCopyOptions dbCopyOptions);
    
    Task CopyDatabaseData(string sourceConnectionString, string destinationConnectionString);
    
    Task CopyDatabaseData(string sourceConnectionString, string destinationConnectionString, DbCopyOptions dbCopyOptions);
    
    Task CopyDatabase(string sourceConnectionString, string destinationConnectionString);
    
    Task CopyDatabase(string sourceConnectionString, string destinationConnectionString, DbCopyOptions dbCopyOptions);
}