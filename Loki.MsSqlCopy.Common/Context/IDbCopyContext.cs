namespace Loki.MsSqlCopy.Common.Context;

public interface IDbCopyContext
{
    DbCopyOptions DbCopyOptions { get; set; }
}