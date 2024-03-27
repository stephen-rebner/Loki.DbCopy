using System.Data.SqlClient;
using Dapper;
using Loki.MsSqlCopy.Data;
using Loki.MsSqlDbCopy.Infrastructure.Repositories.Interfaces;

namespace Loki.MsSqlDbCopy.Infrastructure.Repositories;

public class MsSqlTablesRepository : IMsSqlTablesRepository
{
    public async Task<TableInfo[]> GetTablesAsync(string connectionString)
    {
        await using var sqlConnection = new SqlConnection(connectionString);
        
        var sql = @"
            SELECT 
                SchemaName = s.name,
                TableName = t.name,
                ColumnName = c.name,
                ColumnType = ty.name,
                ColumnMaxLength = c.max_length,
                ColumnPrecision = c.precision,
                ColumnScale = c.scale,
                IsNullable = c.is_nullable
            FROM 
                sys.tables t
            INNER JOIN 
                sys.schemas s ON t.schema_id = s.schema_id
            INNER JOIN 
                sys.columns c ON t.object_id = c.object_id
            INNER JOIN 
                sys.types ty ON c.user_type_id = ty.user_type_id
            WHERE 
                t.is_ms_shipped = 0
            ORDER BY 
                s.name, t.name, c.column_id;
            ";
        
        var tables = await sqlConnection.QueryAsync<TableInfo, ColumnInfo, TableInfo>(
            sql,
            (table, column) =>
            {
                table.Columns.Add(column);
                return table;
            },
            splitOn: nameof(TableInfo.TableName)
        );
        
        return tables?.ToArray() ?? Array.Empty<TableInfo>();
    }
}