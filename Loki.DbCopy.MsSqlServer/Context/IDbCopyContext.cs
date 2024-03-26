using Loki.DbCopy.MsSqlServer;

namespace Loki.DbCopy.Core.Context;

public interface IDbCopyContext
{
    string SourceConnectionString { get;  }
    
    string DestinationConnectionString { get;  }
    
    DbCopyOptions DbCopyOptions { get; }
    
    void SetSourceConnectionString(string sourceConnectionString);
    
    void SetDestinationConnectionString(string destinationConnectionString);
    
    void SetDbCopyOptions(DbCopyOptions dbCopyOptions);
}