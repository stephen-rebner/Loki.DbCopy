namespace Loki.MsSqlCopy.Data;

public record TableInfo
{
    public string SchemaName { get; init; } = string.Empty;
    public string TableName { get; init; } = string.Empty;
    public ICollection<ColumnInfo> Columns { get; } = new List<ColumnInfo>();

    public TableInfo(string schemaName, string tableName)
    {
        SchemaName = schemaName;
        TableName = tableName;
    }
}