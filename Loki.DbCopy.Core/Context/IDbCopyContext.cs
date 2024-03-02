namespace Loki.DbCopy.Core.Context;

public interface IDbCopyContext
{
    string SourceConnectionString { get; set; }
    string DestinationConnectionString { get; set; }
    DbCopyOptions.DbCopyOptions DbCopyOptions { get; set; }
}