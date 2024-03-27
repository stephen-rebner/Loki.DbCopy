using System.Data.SqlClient;
using Dapper;
using Loki.MsSqlDbCopy.Infrastructure.Interfaces;

namespace Loki.MsSqlDbCopy.Infrastructure;

public class IndexesRepository : IIndexesRepository
{
    public async Task<Index[]> LoadIndexes(string connectionString)
    {
       var sql = @"
                SELECT 
                    SchemaName = s.name,
                    TableName = t.name, 
                    IndexName = ind.name, 
                    IndexType = CASE WHEN ind.type = 1 THEN 'Clustered' WHEN ind.type = 2 THEN 'Nonclustered' ELSE 'Other' END,
                    IsUnique = ind.is_unique,
                    IsUniqueConstraint = ind.is_unique_constraint,
                    ColumnName = STRING_AGG(col.name, ', '),
                    IncludedColumnNames = STRING_AGG(CASE WHEN ic.is_included_column = 1 THEN col.name ELSE NULL END, ', ')
                FROM 
                    sys.indexes ind 
                INNER JOIN 
                    sys.index_columns ic ON  ind.object_id = ic.object_id and ind.index_id = ic.index_id 
                INNER JOIN 
                    sys.columns col ON ic.object_id = col.object_id and ic.column_id = col.column_id 
                INNER JOIN 
                    sys.tables t ON ind.object_id = t.object_id 
                INNER JOIN
                    sys.schemas s ON t.schema_id = s.schema_id
                WHERE 
                    ind.is_primary_key = 0 
                    AND t.is_ms_shipped = 0
	                and ind.is_disabled = 0
                GROUP BY 
                    s.name, t.name, ind.name, ind.type, ind.is_unique, ind.is_unique_constraint
                ORDER BY 
                    s.name, t.name, ind.name;
                ";

        await using var connection = new SqlConnection(connectionString);

        var indexes = await connection.QueryAsync<Index>(sql);
        
        return indexes?.ToArray() ?? Array.Empty<Index>();
    }

    public Task CreateIndexes(string connectionString)
    {
        throw new NotImplementedException();
    }
}