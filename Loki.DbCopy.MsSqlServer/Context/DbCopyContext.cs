using System.Data.SqlClient;
using Loki.DbCopy.Core.Context;

namespace Loki.DbCopy.MsSqlServer.Context;

internal class DbCopyContext : IDbCopyContext
{
    public string SourceConnectionString { get; private set; } = string.Empty;

    public string DestinationConnectionString { get; private set; } = string.Empty;

    public DbCopyOptions DbCopyOptions { get; private set; } = new();
    
    public void SetSourceConnectionString(string sourceConnectionString)
    {
        SourceConnectionString = new SqlConnectionStringBuilder(sourceConnectionString).ToString();
    }

    public void SetDestinationConnectionString(string destinationConnectionString)
    {
        DestinationConnectionString = new SqlConnectionStringBuilder(destinationConnectionString).ToString();
    }

    public void SetDbCopyOptions(DbCopyOptions dbCopyOptions)
    {
        DbCopyOptions = dbCopyOptions;
    }
}