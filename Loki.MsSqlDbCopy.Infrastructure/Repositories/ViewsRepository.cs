using System.Data.SqlClient;
using Dapper;
using Loki.MsSqlCopy.Common;
using Loki.MsSqlCopy.Common.Context;
using Loki.MsSqlDbCopy.Infrastructure.Repositories.Interfaces;

namespace Loki.MsSqlDbCopy.Infrastructure.Repositories;

public class ViewsRepository(IConnectionStringContext connectionStringContext) : IViewsRepository
{
    public async Task<string[]> GetViewsAsync()
    {
        var sql =  @"WITH ViewInfo AS (
                    SELECT 
                        o.name, 
                        o.object_id, 
                        m.definition,
                        CASE 
                            WHEN EXISTS (SELECT 1 FROM sys.sql_expression_dependencies WHERE referenced_id = o.object_id) 
                            THEN 1 
                            ELSE 0 
                        END AS HasDependency
                    FROM 
                        sys.objects o
                    JOIN 
                        sys.sql_modules m ON o.object_id = m.object_id
                    WHERE 
                        o.type = 'V'
                )
                SELECT 
                    definition
                FROM 
                    ViewInfo
                    order by HasDependency desc;";

        var sqlConnection = new SqlConnection(connectionStringContext.SourceConnectionString);
        
        var views = await sqlConnection.QueryAsync<string>(sql);
        
        return views.ToArray();
    }

    public async Task SaveViewAsync(string viewSql)
    {
        var sqlConnection = new SqlConnection(connectionStringContext.DestinationConnectionString);
        
        await sqlConnection.ExecuteAsync(viewSql);
    }
}