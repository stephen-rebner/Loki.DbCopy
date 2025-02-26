using System.Data.SqlClient;
using Dapper;
using FluentAssertions;
using Loki.DbCopy.IntegrationTests.BaseIntegrationTests;
using Loki.DbCopy.MsSqlServer;
using Loki.DbCopy.MsSqlServer.Context;
using Loki.MsSqlCopy.Common.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Loki.DbCopy.IntegrationTests.Commands;

public class CopySchemasCommandTests : BaseMsSqlDbCopierIntegrationTests
{
    // use this test as a template for all other integration tests
    [Test]
    public async Task Execute_CopiesAllCustomSchemasFromTheSourceDatabase_WhenTheCreateSchemasOptionIsSetToTrue()
    {
        const string Schema1 = "Schema1";
        const string Schema2 = "Schema2";
        
        // Arrange
        
        const string destinationDatabaseName = "NorthwindDBCopy";
        
        var dbCopyOptions = new DbCopyOptions
        {
            DropAndRecreateDatabase = true,
            // Ensure that we create the schemas
            CreateSchemas = true,
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

        var sourceConnectionString = new SqlConnectionStringBuilder(SourceNorthWindDbContainer.GetConnectionString())
        {
            InitialCatalog = "Northwind"
        };
        
        var destinationConnectionString = new SqlConnectionStringBuilder(DestinationNorthWindDbContainer.GetConnectionString())
        {
            InitialCatalog = destinationDatabaseName
        };
        
        // create 2 schemas in the source database
        await using (var sourceDbConnection = new SqlConnection(sourceConnectionString.ToString()))
        {
            await sourceDbConnection.OpenAsync();

            await sourceDbConnection.ExecuteAsync($"CREATE SCHEMA {Schema1}");

            await sourceDbConnection.ExecuteAsync($"CREATE SCHEMA {Schema2}");
        }

        // Act
        var databaseCopier = ServiceProvider.GetRequiredService<IMsSqlDbCopier>();

        await databaseCopier.Copy(sourceConnectionString, destinationConnectionString, dbCopyOptions);

        // Assert
         
        // create a connection to the destination database
        await using (var destinationDbConnection = new SqlConnection(destinationConnectionString.ToString()))
        {
            var expectedSchemaNames = new [] { Schema1, Schema2 };
            
            var destinationSchemaNames = await destinationDbConnection.QueryAsync<string>(
                    $"SELECT name AS SchemaName FROM sys.schemas where name IN ('{Schema1}', '{Schema2}')");
            
            // assert if the destination schema names contain the expected schema names
            destinationSchemaNames.Should().BeEquivalentTo(expectedSchemaNames);
        }
    }
}