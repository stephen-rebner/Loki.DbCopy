namespace Loki.DbCopy.MsSqlServer.DatabaseCopyFunctions.Interfaces;

public interface IMsSqlDbStructureCopier
{
    Task CopyDatabaseAndSchemas();

    Task CopyDatabaseObjects();
}