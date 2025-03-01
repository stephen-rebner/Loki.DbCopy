using System.Data.SqlClient;
using Dapper;
using FluentAssertions;
using Loki.DbCopy.IntegrationTests.BaseIntegrationTests;
using Loki.DbCopy.MsSqlServer;
using Loki.DbCopy.MsSqlServer.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Loki.DbCopy.IntegrationTests.Commands;

public class CopyViewsCommandTests : BaseMsSqlDbCopierIntegrationTests
{
    [Test]
    public async Task CopyViewsCommand_ShouldCopyAllViews()
    {
        // Arrange
        const string destinationDatabaseName = "NorthwindDBCopy";
        
        var dbCopyOptions = new DbCopyOptions
        {
            CopyData = false,
            CopyStoredProcedures = true
        };
        
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
            var getSourceTableColumnsSql = @"SELECT
                                            m.definition AS ViewDefinition
                                        FROM
                                            sys.objects o
                                        JOIN
                                            sys.sql_modules m ON o.object_id = m.object_id
                                        WHERE
                                            o.type = 'V'  -- Views
                                        ORDER BY
                                            o.name;";
            
            var sourceViews = await sourceConnection.QueryAsync<string>(getSourceTableColumnsSql);

            var destinationViews = await destinationConnection.QueryAsync<string>(getSourceTableColumnsSql);
            
            destinationViews.Should().BeEquivalentTo(sourceViews);
        };
    }
    
}