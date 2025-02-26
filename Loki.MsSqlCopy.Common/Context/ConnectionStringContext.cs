namespace Loki.MsSqlCopy.Common.Context;

public class ConnectionStringContext : IConnectionStringContext
{
    public string SourceConnectionString { get; set; } = string.Empty;

    public string DestinationConnectionString { get; set; } = string.Empty;
}