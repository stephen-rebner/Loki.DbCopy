using System.Data.SqlClient;
using Loki.DbCopy.Core.Context;

namespace Loki.DbCopy.MsSqlServer.Context;

internal class DbCopyContext : IDbCopyContext
{
    public string SourceConnectionString { get; set; } = string.Empty;

    public string DestinationConnectionString { get; set; } = string.Empty;

    public DbCopyOptions DbCopyOptions { get; set; } = new();
}