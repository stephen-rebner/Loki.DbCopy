using System.Text;
using Loki.MsSqlCopy.Data;

namespace Loki.MsSqlDbCopy.Infrastructure.SqlBuilders;

public class CreateTableSqlBuilder(TableInfo tableInfo)
{
    public string Build()
    {
        var sqlBuilder = new StringBuilder();
        sqlBuilder.AppendLine($"CREATE TABLE {tableInfo.SchemaName}.{tableInfo.TableName} (");

        foreach (var column in tableInfo.Columns)
        {
            sqlBuilder.Append($"{column.ColumnName} {column.ColumnType}");

            if (IsTextField(column.ColumnType) && column.ColumnMaxLength.HasValue)
            {
                sqlBuilder.Append($"({column.ColumnMaxLength.Value})");
            }
            
            if (column is { ColumnPrecision: not null, ColumnScale: not null, ColumnType: "decimal" })
            {
                sqlBuilder.Append($"({column.ColumnPrecision.Value}, {column.ColumnScale.Value})");
            }

            if (column.IsNullable)
            {
                sqlBuilder.Append(" NULL");
            }
            else
            {
                sqlBuilder.Append(" NOT NULL");
            }

            if (!string.IsNullOrWhiteSpace(column.DefaultValue))
            {
                sqlBuilder.Append($" DEFAULT {column.DefaultValue}");
            }

            sqlBuilder.AppendLine(",");
        }

        // Remove the last comma and add the closing parenthesis
        sqlBuilder.Remove(sqlBuilder.Length - 3, 1);
        sqlBuilder.AppendLine(");");

        return sqlBuilder.ToString();
    }

    private static bool IsTextField(string columnType)
    {
        return columnType is "char" or "varchar" or "text" or "nchar" or "nvarchar" or "ntext";
    }
}