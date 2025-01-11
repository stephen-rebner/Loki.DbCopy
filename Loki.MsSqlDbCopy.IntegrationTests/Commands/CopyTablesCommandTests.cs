using System.Data.SqlClient;
using Dapper;
using FluentAssertions;
using Loki.DbCopy.IntegrationTests.BaseIntegrationTests;
using Loki.DbCopy.MsSqlServer;
using Loki.DbCopy.MsSqlServer.Commands;
using Loki.DbCopy.MsSqlServer.Context;
using Loki.MsSqlDbCopy.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Loki.DbCopy.IntegrationTests.Commands;

public class CopyTablesCommandTests : BaseMsSqlDbCopierIntegrationTests
{
    
    [Test]
    public async Task Execute_CopiesAllDatabaseTables_WhenTheExcludedTablesOptionIsEmpty()
    {
        // Arrange
        const string destinationDatabaseName = "NorthwindDBCopy";
        
        var dbCopyOptions = new DbCopyOptions
        {
            DropAndRecreateDatabase = true,
            CreateSchemas = true,
            CopyData = false,
            CopyStoredProcedures = false,
            CopyFunctions = false,
            CopyViews = false,
            CopyTriggers = false,
            CopyIndexes = false,
            CopyPrimaryKeys = false,
            CopyForeignKeys = false,
            CopyTables = true
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
            var sourceTableCount = await sourceConnection.QueryFirstOrDefaultAsync<int>("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES");
            var destinationTableCount = await destinationConnection.QueryFirstOrDefaultAsync<int>("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES");
            
            destinationTableCount.Should().Be(sourceTableCount);
        };
    }
}