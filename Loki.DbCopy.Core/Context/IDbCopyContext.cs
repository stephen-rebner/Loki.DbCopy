namespace Loki.DbCopy.Core.Context;

public interface IDbCopyContext
{
    string SourceConnectionString { get;  }
    
    string DestinationConnectionString { get;  }
    
    DbCopyOptions.DbCopyOptions DbCopyOptions { get; }
    
    void SetSourceConnectionString(string sourceConnectionString);
    
    void SetDestinationConnectionString(string destinationConnectionString);
    
    void SetDbCopyOptions(DbCopyOptions.DbCopyOptions dbCopyOptions);
}