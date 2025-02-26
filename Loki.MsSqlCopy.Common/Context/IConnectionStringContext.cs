namespace Loki.MsSqlCopy.Common.Context;

public interface IConnectionStringContext
{
    public string SourceConnectionString { get; set; }

    public string DestinationConnectionString { get; set; }
}