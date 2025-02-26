using System.Data.SqlClient;
using Loki.DbCopy.MsSqlServer.Context;
using Loki.MsSqlCopy.Common.Context;

namespace Loki.DbCopy.MsSqlServer;

public interface IMsSqlDbCopier
{
    Task Copy(SqlConnectionStringBuilder sourceConnectionString, SqlConnectionStringBuilder destinationConnectionString);
    
    Task Copy(SqlConnectionStringBuilder sourceConnectionString, SqlConnectionStringBuilder destinationConnectionString, DbCopyOptions dbCopyOptions);
}