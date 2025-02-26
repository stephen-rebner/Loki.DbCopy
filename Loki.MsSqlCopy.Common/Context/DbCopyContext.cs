namespace Loki.MsSqlCopy.Common.Context;

public class DbCopyContext : IDbCopyContext
{
    public DbCopyOptions DbCopyOptions { get; set; } = new();
}