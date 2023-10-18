namespace Loki.DbCopy;

public interface IDatabaseCopier
{
    void Copy(string sourceConnectionString, string destinationConnectionString);
}