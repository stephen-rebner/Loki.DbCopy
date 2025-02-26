using System.Data.SqlClient;
using Dapper;
using FluentAssertions;
using Loki.DbCopy.IntegrationTests.BaseIntegrationTests;
using Loki.DbCopy.MsSqlServer;
using Microsoft.Extensions.DependencyInjection;

namespace Loki.DbCopy.IntegrationTests.Commands;

public class CopyTablesCommandTests : BaseMsSqlDbCopierIntegrationTests
{
    
    [Test]
    public async Task Execute_CopiesAllDatabaseTables_WhenCopyTablesIsSetToTrue()
    {
        // Arrange
        const string destinationDatabaseName = "NorthwindDBCopy";
        
        var dbCopyOptions = new DbCopyOptions
        {
            CopyData = false,
            CopyStoredProcedures = false,
            CopyFunctions = false,
            CopyViews = false,
            CopyTriggers = false,
            CopyIndexes = false,
            CopyPrimaryKeys = false,
            CopyForeignKeys = false,
            CopyTables = true // set to true to copy tables
        };
        
        // use the SqlConnectionStringBuilder to build connection string
        var sourceConnectionStringBuilder = new SqlConnectionStringBuilder(SourceNorthWindDbContainer.GetConnectionString())
        {
            InitialCatalog = "Northwind"
        };;

        var destinationConnectionStringBuilder = new SqlConnectionStringBuilder(DestinationNorthWindDbContainer.GetConnectionString())
        {
            InitialCatalog = destinationDatabaseName
        };

        var msSqlDbCopier = ServiceProvider.GetService<IMsSqlDbCopier>();
        
        // Act
        await msSqlDbCopier.Copy(sourceConnectionStringBuilder, destinationConnectionStringBuilder, dbCopyOptions);
        
        // Assert
        await using var sourceConnection = new SqlConnection(sourceConnectionStringBuilder.ToString());
        await using (var destinationConnection = new SqlConnection(destinationConnectionStringBuilder.ToString()))
        {
            var getTableCountSql = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";
            
            var sourceTableCount = await sourceConnection.QueryFirstOrDefaultAsync<int>(getTableCountSql);
            var destinationTableCount = await destinationConnection.QueryFirstOrDefaultAsync<int>(getTableCountSql);
            
            destinationTableCount.Should().Be(sourceTableCount);
        };
    }
    
    [Test]
    public async Task Execute_CopiesAllDatabaseTablesColumns_WhenCopyTablesIsTrue()
    {
        // Arrange
        const string destinationDatabaseName = "NorthwindDBCopy";
        
        var dbCopyOptions = new DbCopyOptions
        {
            CopyData = false,
            CopyStoredProcedures = false,
            CopyFunctions = false,
            CopyViews = false,
            CopyTriggers = false,
            CopyIndexes = false,
            CopyPrimaryKeys = false,
            CopyForeignKeys = false,
            CopyTables = true // set to true to copy tables
        };
        
        // use the SqlConnectionStringBuilder to build connection string
        var sourceConnectionStringBuilder = new SqlConnectionStringBuilder(SourceNorthWindDbContainer.GetConnectionString())
        {
            InitialCatalog = "Northwind"
        };

        var destinationConnectionStringBuilder = new SqlConnectionStringBuilder(DestinationNorthWindDbContainer.GetConnectionString())
        {
            InitialCatalog = destinationDatabaseName
        };

        var msSqlDbCopier = ServiceProvider.GetService<IMsSqlDbCopier>();
        
        // Act
        await msSqlDbCopier.Copy(sourceConnectionStringBuilder, destinationConnectionStringBuilder, dbCopyOptions);
        
        // Assert
        await using var sourceConnection = new SqlConnection(sourceConnectionStringBuilder.ToString());
        await using (var destinationConnection = new SqlConnection(destinationConnectionStringBuilder.ToString()))
        {
            var getSourceTableColumnsSql = @"SELECT t.TABLE_NAME, COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS c
                                            inner join  INFORMATION_SCHEMA.TABLES t on t.TABLE_NAME = c.TABLE_NAME and t.TABLE_SCHEMA = c.TABLE_SCHEMA 
                                            where t.TABLE_TYPE = 'BASE TABLE'";
            
            var sourceTableColumns = await sourceConnection.QueryAsync(getSourceTableColumnsSql);

            var destinationTableColumns = await destinationConnection.QueryAsync(getSourceTableColumnsSql);
            
            destinationTableColumns.Should().BeEquivalentTo(sourceTableColumns);
        };
    }
    
    // add a test to check that no tables are copied when CopyTables is set to false
    [Test]
    public async Task Execute_DoesNotCopyAnyTables_WhenCopyTablesIsSetToFalse()
    {
        // Arrange
        const string destinationDatabaseName = "NorthwindDBCopy";
        
        var dbCopyOptions = new DbCopyOptions
        {
            CopyData = false,
            CopyStoredProcedures = false,
            CopyFunctions = false,
            CopyViews = false,
            CopyTriggers = false,
            CopyIndexes = false,
            CopyPrimaryKeys = false,
            CopyForeignKeys = false,
            CopyTables = false // set to false to not copy tables
        };
        
        // use the SqlConnectionStringBuilder to build connection string
        var sourceConnectionStringBuilder = new SqlConnectionStringBuilder(SourceNorthWindDbContainer.GetConnectionString())
        {
            InitialCatalog = "Northwind"
        };

        var destinationConnectionStringBuilder = new SqlConnectionStringBuilder(DestinationNorthWindDbContainer.GetConnectionString())
        {
            InitialCatalog = destinationDatabaseName
        };

        var msSqlDbCopier = ServiceProvider.GetService<IMsSqlDbCopier>();
        
        // Act
        await msSqlDbCopier.Copy(sourceConnectionStringBuilder, destinationConnectionStringBuilder, dbCopyOptions);
        
        // Assert
        await using var sourceConnection = new SqlConnection(sourceConnectionStringBuilder.ToString());
        await using (var destinationConnection = new SqlConnection(destinationConnectionStringBuilder.ToString()))
        {
            var getTableCountSql = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";

            var destinationTableCount = await destinationConnection.QueryFirstOrDefaultAsync<int>(getTableCountSql);
            
            destinationTableCount.Should().Be(0);
        };
    }
}