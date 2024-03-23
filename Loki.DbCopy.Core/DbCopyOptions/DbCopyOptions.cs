namespace Loki.DbCopy.Core.DbCopyOptions;

public class DbCopyOptions
{
    public bool DropAndRecreateDatabase { get; set; } = true;
    public bool CreateSchemas { get; set; } = true;
    public bool CopyData { get; set; } = true;
    public bool CopyIndexes { get; set; } = true;
    public bool CopyForeignKeys { get; set; } = true;
    public bool CopyTriggers { get; set; } = true;
    public bool CopyStoredProcedures { get; set; } = true;
    public bool CopyViews { get; set; } = true;
    public bool CopyFunctions { get; set; } = true;
    public bool CopyUsers { get; set; } = false;
    public bool CopyRoles { get; set; } = false;
    public bool CopyLogins { get; set; } = false;
    public bool CopyPermissions { get; set; } = false;

    public string[]? ExcludedTables { get; set; }
    public string[]? ExcludedViews { get; set; }
    public string[]? ExcludedStoredProcedures { get; set; }
    public string[]? ExcludedFunctions { get; set; }
    public string[]? ExcludedUsers { get; set; }
    public string[]? ExcludedRoles { get; set; }
    public string[]? ExcludedLogins { get; set; }
    public string[]? ExcludedPermissions { get; set; }
    public string[]? ExcludedDatabaseSettings { get; set; }
    public string[]? ExcludedDatabaseOptions { get; set; }
}