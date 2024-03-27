namespace Loki.MsSqlCopy.Data;

public record TableInfo(
    string SchemaName, 
    string TableName, 
    ColumnInfo[] Columns);