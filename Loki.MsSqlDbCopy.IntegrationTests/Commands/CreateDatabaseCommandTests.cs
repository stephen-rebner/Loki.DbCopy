using System.Data.SqlClient;
using Dapper;
using FluentAssertions;
using Loki.DbCopy.Core.Context;
using Loki.DbCopy.IntegrationTests.BaseIntegrationTests;
using Loki.DbCopy.MsSqlServer.Commands;
using Loki.DbCopy.MsSqlServer.Commands.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Loki.DbCopy.IntegrationTests.Commands;

public class CreateDatabaseCommandTests : BaseMsSqlDbCopierIntegrationTests
{
    
    [Test]
    public async Task Execute_CreatesAnExistingDatabase_WhenDropDatabaseAndRecreateDatabaseOptionIsTrue()
    {
        // Arrange
        const string destinationDatabaseName = "NorthwindDBCopy";
        
        var dbCopyContext = ServiceProvider.GetRequiredService<IDbCopyContext>();
        
        // Ensure that we drop and recreate the database
        dbCopyContext.DbCopyOptions.DropAndRecreateDatabase = true;

        dbCopyContext.SourceConnectionString = SourceNorthWindDbContainer.GetConnectionString();

        // Act
        dbCopyContext.DestinationConnectionString = @$"Server={DestinationNorthWindDbContainer.Hostname},{DestinationNorthWindDbContainer.GetMappedPublicPort(1433)};
                        Database={destinationDatabaseName};
                        User Id={UserId};
                        Password={Password}";
        
        var createDatabaseIfExistsCommand = ServiceProvider
            .GetServices<IDatabaseCopyCommand>()
            .First(command => command.GetType() == typeof(CreateDatabaseCommand));
        
        await createDatabaseIfExistsCommand.Execute();
        
        // Assert
        await using var connection = new SqlConnection(dbCopyContext.DestinationConnectionString);
        
        var destinationDbCount = await connection.QueryFirstOrDefaultAsync<int>($"SELECT COUNT(*) FROM sys.databases WHERE name = '{destinationDatabaseName}'");

        destinationDbCount.Should().Be(1);
    }
}