namespace Loki.DbCopy.MsSqlServer;

public interface IMsSqlDbCopier
{
    Task Copy(string sourceConnectionString, string destinationConnectionString);
    
    Task Copy(string sourceConnectionString, string destinationConnectionString, DbCopyOptions dbCopyOptions);
}