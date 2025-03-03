using System.Data.SqlClient;
using Dapper;
using FluentAssertions;
using Loki.DbCopy.IntegrationTests.BaseIntegrationTests;
using Loki.DbCopy.MsSqlServer;
using Loki.DbCopy.MsSqlServer.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Loki.DbCopy.IntegrationTests.Commands;

public class CopyDataCommandTests : BaseMsSqlDbCopierIntegrationTests
{
    [Test]
    public async Task CopyDataCommand_ShouldCopyAllData()
    {
        // Arrange
        const string destinationDatabaseName = "NorthwindDBCopy";
    
        var sourceConnectionStringBuilder = new SqlConnectionStringBuilder(SourceNorthWindDbContainer.GetConnectionString())
        {
            InitialCatalog = "Northwind"
        };
    
        var destinationConnectionStringBuilder = new SqlConnectionStringBuilder(DestinationNorthWindDbContainer.GetConnectionString())
        {
            InitialCatalog = destinationDatabaseName
        };
    
        await using var sourceConnection = new SqlConnection(sourceConnectionStringBuilder.ToString());
        await sourceConnection.OpenAsync();
    
        var msSqlDbCopier = ServiceProvider.GetService<IMsSqlDbCopier>();
    
        // Act
        await msSqlDbCopier.Copy(sourceConnectionStringBuilder, destinationConnectionStringBuilder);
    
        // Assert
        await using var sourceNorthwindConnection = new SqlConnection(sourceConnectionStringBuilder.ToString());
        await using var destinationNorthwindConnection = new SqlConnection(destinationConnectionStringBuilder.ToString())
        {
            // check that the destination tables have the exact same rows as the source tables
            
            
        };
    }
}