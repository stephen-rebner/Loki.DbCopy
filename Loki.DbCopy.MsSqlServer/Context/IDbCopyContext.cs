namespace Loki.DbCopy.MsSqlServer.Context;

public interface IDbCopyContext
{
    DbCopyOptions DbCopyOptions { get; set; }
}