using System.Data.SqlClient;
using Dapper;
using Loki.MsSqlCopy.Data;
using Loki.MsSqlDbCopy.Infrastructure.Repositories.Interfaces;
using Loki.MsSqlDbCopy.Infrastructure.SqlBuilders;

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

        var tableDictionary = new Dictionary<string, TableInfo>();

        var tables = await sqlConnection.QueryAsync<TableInfo, ColumnInfo, TableInfo>(
            sql,
            (table, column) =>
            {
                if (!tableDictionary.TryGetValue(table.TableName, out var currentTable))
                {
                    tableDictionary.Add(table.TableName, table);
                }
                table.Columns.Add(column);
                return table;
            },
            splitOn: "ColumnName"
        );

        return tableDictionary.Values.ToArray();
    }

    public async Task SaveTableAsync(string connectionString, TableInfo tableInfo)
    {
        var createTableSqlBuilder = new CreateTableSqlBuilder(tableInfo);
        
        var createTableSql = createTableSqlBuilder.Build();
        
        await using var sqlConnection = new SqlConnection(connectionString);
        
        await sqlConnection.ExecuteAsync(createTableSql);
        
    }
}