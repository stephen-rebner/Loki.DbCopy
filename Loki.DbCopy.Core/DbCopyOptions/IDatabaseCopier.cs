namespace Loki.DbCopy.Core.DbCopyOptions;

internal interface IDatabaseCopier
{
    internal void Copy(string sourceConnectionString, string destinationConnectionString);
    internal void Copy(string sourceConnectionString, string destinationConnectionString, DbCopyOptions dbCopyOptions);
}