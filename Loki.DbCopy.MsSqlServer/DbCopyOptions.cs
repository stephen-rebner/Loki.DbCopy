﻿namespace Loki.DbCopy.MsSqlServer;

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

    public string[] ExcludedTables { get; set; } = Array.Empty<string>();
    public string[] ExcludedViews { get; set; } = Array.Empty<string>();
    public string[] ExcludedStoredProcedures { get; set; } = Array.Empty<string>();
    public string[] ExcludedFunctions { get; set; } = Array.Empty<string>();
    public string[] ExcludedUsers { get; set; } = Array.Empty<string>();
    public string[] ExcludedRoles { get; set; } = Array.Empty<string>();
    public string[] ExcludedLogins { get; set; } = Array.Empty<string>();
    public string[] ExcludedPermissions { get; set; } = Array.Empty<string>();
    public string[] ExcludedDatabaseSettings { get; set; } = Array.Empty<string>();
    public string[] ExcludedDatabaseOptions { get; set; } = Array.Empty<string>();
}