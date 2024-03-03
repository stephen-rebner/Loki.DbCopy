namespace Loki.DbCopy.Core;

public interface IDatabaseCopier
{
    public Task Copy(string sourceConnectionString, string destinationConnectionString);
    public Task Copy(string sourceConnectionString, string destinationConnectionString, DbCopyOptions.DbCopyOptions dbCopyOptions);
}