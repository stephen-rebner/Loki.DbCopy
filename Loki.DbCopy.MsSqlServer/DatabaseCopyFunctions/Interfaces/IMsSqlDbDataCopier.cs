namespace Loki.DbCopy.MsSqlServer.DatabaseCopyFunctions.Interfaces;

public interface IMsSqlDbDataCopier
{
    Task CopyDatabaseData();
}