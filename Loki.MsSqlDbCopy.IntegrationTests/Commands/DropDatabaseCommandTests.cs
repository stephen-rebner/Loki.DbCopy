using System.Data.SqlClient;
using Dapper;
using FluentAssertions;
using Loki.DbCopy.Core.Commands;
using Loki.DbCopy.Core.Context;
using Loki.DbCopy.IntegrationTests.BaseIntegrationTests;
using Loki.DbCopy.MsSqlServer.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Loki.DbCopy.IntegrationTests.Commands;

public class DropDatabaseCommandTests : BaseMsSqlDbCopierIntegrationTests
{
    
    [Test]
    public async Task Execute_DropsAnExistingDatabase_WhenDatabaseExists_AndDropDatabaseAndRecreateDatabaseIsTrue()
    {
        // Arrange
        const string destinationDatabaseName = "NorthwindDBCopy";
        
        var dbCopyContext = ServiceProvider.GetRequiredService<IDbCopyContext>();
        
        // Ensure that we drop and recreate the database
        dbCopyContext.DbCopyOptions.DropAndRecreateDatabase = true;

        dbCopyContext.SetSourceConnectionString(SourceNorthWindDbContainer.GetConnectionString());

        dbCopyContext.SetDestinationConnectionString(DestinationNorthWindDbContainer.GetConnectionString());
        
        // Create an empty destination database to be dropped
        await using var connection = new SqlConnection(dbCopyContext.DestinationConnectionString);

        await connection.OpenAsync();
        
        await connection.ExecuteAsync($"CREATE DATABASE {destinationDatabaseName}");
        
        // Act
        dbCopyContext.SetDestinationConnectionString(
                        @$"Server={DestinationNorthWindDbContainer.Hostname},{DestinationNorthWindDbContainer.GetMappedPublicPort(1433)};
                        Database={destinationDatabaseName};
                        User Id={UserId};
                        Password={Password}");
        
        var dropDatabaseIfExistsCommand = ServiceProvider
            .GetServices<IDatabaseCopyCommand>()
            .First(command => command.GetType() == typeof(DropDatabaseCommand));
        
        await dropDatabaseIfExistsCommand.Execute();
        
        // Assert
        var destinationDbCount = await connection.QueryFirstOrDefaultAsync<int>($"SELECT COUNT(*) FROM sys.databases WHERE name = '{destinationDatabaseName}'");

        destinationDbCount.Should().Be(0);
    }
    
    [Test]
    public async Task Execute_DoesNotDropAnExistingDatabase_WhenDatabaseExists_AndDropDatabaseAndRecreateDatabaseIsFalse()
    {
        // Arrange
        const string destinationDatabaseName = "NorthwindDBCopy";
        
        var dbCopyContext = ServiceProvider.GetRequiredService<IDbCopyContext>();
        
        // Ensure that we drop and recreate the database
        dbCopyContext.DbCopyOptions.DropAndRecreateDatabase = false;

        dbCopyContext.SetSourceConnectionString(SourceNorthWindDbContainer.GetConnectionString());

        dbCopyContext.SetDestinationConnectionString(DestinationNorthWindDbContainer.GetConnectionString());
        
        // Create an empty destination database to be dropped
        await using var connection = new SqlConnection(dbCopyContext.DestinationConnectionString);

        await connection.OpenAsync();
        
        await connection.ExecuteAsync($"CREATE DATABASE {destinationDatabaseName}");
        
        // Act
        dbCopyContext.SetDestinationConnectionString(
                        @$"Server={DestinationNorthWindDbContainer.Hostname},{DestinationNorthWindDbContainer.GetMappedPublicPort(1433)};
                        Database={destinationDatabaseName};
                        User Id={UserId};
                        Password={Password}");
        
        var dropDatabaseIfExistsCommand = ServiceProvider
            .GetServices<IDatabaseCopyCommand>()
            .First(command => command.GetType() == typeof(DropDatabaseCommand));
        
        await dropDatabaseIfExistsCommand.Execute();
        
        // Assert
        var destinationDbCount = await connection.QueryFirstOrDefaultAsync<int>($"SELECT COUNT(*) FROM sys.databases WHERE name = '{destinationDatabaseName}'");

        destinationDbCount.Should().Be(1); // The database should still exist
    }
}