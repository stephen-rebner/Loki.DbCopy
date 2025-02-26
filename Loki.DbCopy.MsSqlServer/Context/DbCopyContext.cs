namespace Loki.DbCopy.MsSqlServer.Context;

public class DbCopyContext : IDbCopyContext
{
    public DbCopyOptions DbCopyOptions { get; set; } = new();
}