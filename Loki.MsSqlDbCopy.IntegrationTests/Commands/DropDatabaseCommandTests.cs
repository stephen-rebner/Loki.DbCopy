using System.Data.SqlClient;
using Dapper;
using FluentAssertions;
using Loki.DbCopy.IntegrationTests.BaseIntegrationTests;
using Loki.DbCopy.MsSqlServer.Commands;
using Loki.DbCopy.MsSqlServer.Commands.Interfaces;
using Loki.MsSqlCopy.Common.Context;
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
        var connectionStringContext = ServiceProvider.GetRequiredService<IConnectionStringContext>();
        
        // Ensure that we drop and recreate the database
        dbCopyContext.DbCopyOptions.DropAndRecreateDatabase = true;

        connectionStringContext.SourceConnectionString = SourceNorthWindDbContainer.GetConnectionString();

        connectionStringContext.DestinationConnectionString = DestinationNorthWindDbContainer.GetConnectionString();
        
        // Create an empty destination database to be dropped
        await using var connection = new SqlConnection(connectionStringContext.DestinationConnectionString);

        await connection.OpenAsync();
        
        await connection.ExecuteAsync($"CREATE DATABASE {destinationDatabaseName}");
        
        // Act
        connectionStringContext.DestinationConnectionString = 
                        @$"Server={DestinationNorthWindDbContainer.Hostname},{DestinationNorthWindDbContainer.GetMappedPublicPort(1433)};
                        Database={destinationDatabaseName};
                        User Id={UserId};
                        Password={Password}";
        
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
        var connectionStringContext = ServiceProvider.GetRequiredService<IConnectionStringContext>();
        
        // Ensure that we drop and recreate the database
        dbCopyContext.DbCopyOptions.DropAndRecreateDatabase = false;

        connectionStringContext.SourceConnectionString = SourceNorthWindDbContainer.GetConnectionString();

        connectionStringContext.DestinationConnectionString = DestinationNorthWindDbContainer.GetConnectionString();
        
        // Create an empty destination database to be dropped
        await using var connection = new SqlConnection(connectionStringContext.DestinationConnectionString);

        await connection.OpenAsync();
        
        await connection.ExecuteAsync($"CREATE DATABASE {destinationDatabaseName}");
        
        // Act
        connectionStringContext.DestinationConnectionString = 
                        @$"Server={DestinationNorthWindDbContainer.Hostname},{DestinationNorthWindDbContainer.GetMappedPublicPort(1433)};
                        Database={destinationDatabaseName};
                        User Id={UserId};
                        Password={Password}";
        
        var dropDatabaseIfExistsCommand = ServiceProvider
            .GetServices<IDatabaseCopyCommand>()
            .First(command => command.GetType() == typeof(DropDatabaseCommand));
        
        await dropDatabaseIfExistsCommand.Execute();
        
        // Assert
        var destinationDbCount = await connection.QueryFirstOrDefaultAsync<int>($"SELECT COUNT(*) FROM sys.databases WHERE name = '{destinationDatabaseName}'");

        destinationDbCount.Should().Be(1); // The database should still exist
    }
}