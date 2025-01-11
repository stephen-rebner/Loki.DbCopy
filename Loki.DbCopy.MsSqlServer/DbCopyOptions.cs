namespace Loki.DbCopy.MsSqlServer;

public class DbCopyOptions
{
    public bool DropAndRecreateDatabase { get; set; } = true;
    public bool CreateSchemas { get; set; } = true;
    public bool CopyData { get; set; } = true;
    public bool CopyIndexes { get; set; } = true;
    public bool CopyPrimaryKeys { get; set; } = true;
    public bool CopyForeignKeys { get; set; } = true;
    public bool CopyTriggers { get; set; } = true;
    public bool CopyStoredProcedures { get; set; } = true;
    public bool CopyViews { get; set; } = true;
    public bool CopyFunctions { get; set; } = true;
    // public bool CopyUsers { get; set; } = false;
    // public bool CopyRoles { get; set; } = false;
    // public bool CopyLogins { get; set; } = false;
    public bool CopyTables { get; set; }

    // public bool CopyPermissions { get; set; } = false;

    public string[] ExcludedTables { get; set; } = Array.Empty<string>();
    public string[] ExcludedViews { get; set; } = Array.Empty<string>();
    public string[] ExcludedStoredProcedures { get; set; } = Array.Empty<string>();
    public string[] ExcludedFunctions { get; set; } = Array.Empty<string>();
    public string[] ExcludedSchemas { get; set; } = Array.Empty<string>();
}