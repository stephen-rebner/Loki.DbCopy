using System.Data.SqlClient;

namespace Loki.DbCopy.MsSqlServer;

public interface IMsSqlDbCopier
{
    Task Copy(SqlConnectionStringBuilder sourceConnectionString, SqlConnectionStringBuilder destinationConnectionString);
    
    Task Copy(SqlConnectionStringBuilder sourceConnectionString, SqlConnectionStringBuilder destinationConnectionString, DbCopyOptions dbCopyOptions);
}