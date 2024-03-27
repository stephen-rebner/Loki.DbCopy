namespace Loki.MsSqlCopy.Data;

public record TableInfo
{
    public string SchemaName { get; init; } = string.Empty;
    public string TableName { get; init; } = string.Empty;
    public ICollection<ColumnInfo> Columns { get; init; } = new List<ColumnInfo>();
}