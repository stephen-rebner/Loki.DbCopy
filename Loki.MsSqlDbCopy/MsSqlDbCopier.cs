using CommunityToolkit.Diagnostics;

namespace Loki.DbCopy;



public class MsSqlDbCopier : IDatabaseCopier
{
    private readonly IDbCopyContext _dbCopyContext;
    private readonly IEnumerable<IDatabaseCopyFunction> _databaseCopyFunctions;

    public MsSqlDbCopier(IDbCopyContext dbCopyContext, IEnumerable<IDatabaseCopyFunction> databaseCopyFunctions)
    {
        _dbCopyContext = dbCopyContext;
        _databaseCopyFunctions = databaseCopyFunctions;
    }

    public void Copy(string sourceConnectionString, string destinationConnectionString)
    {
        Copy(sourceConnectionString, destinationConnectionString, new DbCopyOptions());
    }

    public void Copy(string sourceConnectionString, string destinationConnectionString, DbCopyOptions dbCopyOptions)
    {
        Guard.IsNotNullOrEmpty(sourceConnectionString);
        Guard.IsNotNullOrEmpty(destinationConnectionString);
        Guard.IsNotNull(dbCopyOptions);

        _dbCopyContext.SourceConnectionString = sourceConnectionString;
        _dbCopyContext.DestinationConnectionString = destinationConnectionString;
        _dbCopyContext.DbCopyOptions = dbCopyOptions;

        foreach (var databaseCopyFunction in _databaseCopyFunctions)
        {
            databaseCopyFunction.Copy();
        }
    }

    public interface IDatabaseCopyFunction
    {
        void Copy();
    }

    public class DataCopyFunction : IDatabaseCopyFunction
    {
        public void Copy()
        {
            throw new NotImplementedException();
        }
    }

    public class DbCopyContext : IDbCopyContext
    {
        public string SourceConnectionString { get; set; }
        public string DestinationConnectionString { get; set; }
        public DbCopyOptions DbCopyOptions { get; set; }

        public DbCopyContext(string sourceConnectionString, string destinationConnectionString)
        {
            SourceConnectionString = sourceConnectionString;
            DestinationConnectionString = destinationConnectionString;
            DbCopyOptions = new DbCopyOptions();
        }
    }

    public interface IDbCopyContext
    {
        string SourceConnectionString { get; set; }
        string DestinationConnectionString { get; set; }
        DbCopyOptions DbCopyOptions { get; set; }
    }

    public class DbCopyOptions
    {
        public bool DropDatabaseIfExists { get; set; } = true;
        public bool CopyData { get; set; } = true;
        public bool CopySchema { get; set; } = true;
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
        public bool CopyDatabaseSettings { get; set; } = false;
        public bool CopyDatabaseOptions { get; set; } = false;
        public bool CopyDatabaseFiles { get; set; } = false;
        public bool CopyDatabaseFileGroups { get; set; } = false;

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
}

// public class DbCopyOptionsBuilder 
// {
//     static DbCopyOptionsBuilder()
//     {
//         var options = new DbCopyOptionsBuilder()
//             .CopyData()
//             .CopySchema()
//             .CopyIndexes()
//             .CopyForeignKeys()
//             .CopyTriggers()
//             .CopyStoredProcedures()
//             .CopyViews()
//             .CopyFunctions()
//             .CopyUsers()
//             .CopyRoles()
//             .CopyLogins()
//             .CopyPermissions()
//             .CopyDatabaseSettings()
//             .CopyDatabaseOptions()
//             .CopyDatabaseFiles()
//             .CopyDatabaseFileGroups()
//             .ExcludeTables()
//             .ExcludeViews()
//             .ExcludeStoredProcedures()
//             .ExcludeFunctions()
//             .ExcludeUsers()
//             .ExcludeRoles()
//             .ExcludeLogins()
//             .ExcludePermissions()
//             .ExcludeDatabaseSettings()
//             .ExcludeDatabaseOptions()
//             .Build();
//         
//         
// }