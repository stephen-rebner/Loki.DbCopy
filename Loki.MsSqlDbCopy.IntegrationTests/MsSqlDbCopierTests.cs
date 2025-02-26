using System.Data.SqlClient;
using Dapper;
using FluentAssertions;
using Loki.DbCopy.IntegrationTests.BaseIntegrationTests;
using Loki.DbCopy.MsSqlServer;
using Loki.DbCopy.MsSqlServer.Context;
using Loki.MsSqlCopy.Common.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Loki.DbCopy.IntegrationTests;

public class MsSqlDbCopierTests : BaseMsSqlDbCopierIntegrationTests
{
    [Test]
    public async Task Copy_CopysTheDatabase_WhenTheSourceAndDestinationConnectionStringsAreProvided()
    {
        // Arrange
        const string destinationDatabaseName = "NorthwindDBCopy";
        
        var sourceConnectionString = SourceNorthWindDbContainer.GetConnectionString();
        var destinationConnectionString = DestinationNorthWindDbContainer.GetConnectionString();

        var sourceConnectionStringBuilder = new SqlConnectionStringBuilder(sourceConnectionString)
        {
            InitialCatalog = "Northwind"
        };

        var destinationConnectionStringBuilder = new SqlConnectionStringBuilder(destinationConnectionString)
        {
            InitialCatalog = destinationDatabaseName
        };
        
        var dbCopyOptions = new DbCopyOptions
        {
            DropAndRecreateDatabase = true,
            CreateSchemas = false,
            CopyData = false,
            CopyStoredProcedures = false,
            CopyFunctions = false,
            CopyViews = false,
            CopyTriggers = false,
            CopyIndexes = false,
            CopyPrimaryKeys = false,
            CopyForeignKeys = false,
            CopyTables = false
        };
        
        // Act
        var databaseCopier = ServiceProvider.GetRequiredService<IMsSqlDbCopier>();
        
        await databaseCopier.Copy(sourceConnectionStringBuilder, destinationConnectionStringBuilder, dbCopyOptions);
        
        // Assert
        await using var connection = new SqlConnection(destinationConnectionStringBuilder.ToString());
        
        var destinationDbCount = await connection.QueryFirstOrDefaultAsync<int>($"SELECT COUNT(*) FROM sys.databases WHERE name = '{destinationDatabaseName}'");

        destinationDbCount.Should().Be(1);
    }
}