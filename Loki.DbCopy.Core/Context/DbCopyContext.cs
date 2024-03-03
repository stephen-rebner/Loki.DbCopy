namespace Loki.DbCopy.Core.Context;

public class DbCopyContext : IDbCopyContext
{
    public string SourceConnectionString { get; set; } = string.Empty;

    public string DestinationConnectionString { get; set; } = string.Empty;

    public DbCopyOptions.DbCopyOptions DbCopyOptions { get; set; } = new();
}