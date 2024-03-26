namespace Loki.DbCopy.MsSqlServer.DatabaseCopyFunctions.Interfaces;

public interface IMsSqlDbStructureCopier
{
    Task CopyDatabaseStructure();
}