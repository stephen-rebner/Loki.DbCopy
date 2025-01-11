namespace Loki.MsSqlCopy.Data;

public record ColumnInfo
{
    public string ColumnName { get; init; } = string.Empty;
    public string ColumnType { get; init; } = string.Empty;
    public int? ColumnMaxLength { get; init; }
    public int? ColumnPrecision { get; init; }
    public int? ColumnScale { get; init; }
    public bool IsNullable { get; init; }
    public string DefaultValue { get; init; } = string.Empty;
}