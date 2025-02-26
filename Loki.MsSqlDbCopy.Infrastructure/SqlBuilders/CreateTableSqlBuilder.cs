using System.Text;
using Loki.MsSqlCopy.Common;

namespace Loki.MsSqlDbCopy.Infrastructure.SqlBuilders;

public class CreateTableSqlBuilder(TableInfo tableInfo)
{
    public string Build()
    {
        var sqlBuilder = new StringBuilder();
        sqlBuilder.AppendLine($"CREATE TABLE [{tableInfo.SchemaName}].[{tableInfo.TableName}] (");

        foreach (var column in tableInfo.Columns)
        {
            sqlBuilder.Append($"[{column.ColumnName}] ");
            sqlBuilder.Append('['); // open square bracket to escape column names
            sqlBuilder.Append($"{column.ColumnType}");
            
            if (column is { ColumnPrecision: not null, ColumnScale: not null, ColumnType: "decimal" })
            {
                sqlBuilder.Append($"({column.ColumnPrecision.Value}, {column.ColumnScale.Value})");
            }

            sqlBuilder.Append(']'); // close square bracket to escape column names
            
            if (IsTextFieldWithMaxLength(column.ColumnType) && column.ColumnMaxLength.HasValue)
            {
                sqlBuilder.Append($"({column.ColumnMaxLength.Value})");
            }

            sqlBuilder.Append(column.IsNullable ? " NULL" : " NOT NULL");

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

    private static bool IsTextFieldWithMaxLength(string columnType)
    {
        return columnType is "char" or "varchar" or "text" or "nchar" or "nvarchar";
    }
}