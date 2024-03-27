namespace Loki.MsSqlCopy.Data;

public record IndexInfo(
    string SchemaName,
    string TableName,
    string IndexName,
    string IndexType,
    bool IsUnique,
    bool IsUniqueConstraint,
    string[] ColumnName,
    string[] IncludedColumnNames);