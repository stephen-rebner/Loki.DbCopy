using System.Data.SqlClient;
using Dapper;
using FluentAssertions;
using Loki.DbCopy.IntegrationTests.BaseIntegrationTests;
using Loki.DbCopy.MsSqlServer;
using Loki.DbCopy.MsSqlServer.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Loki.DbCopy.IntegrationTests.Commands;

public class CopyStoredProceduresTests : BaseMsSqlDbCopierIntegrationTests
{
    // add an nunit test to check the stored procedures are copied
    [Test]
    public async Task CopyStoredProceduresCommand_ShouldCopyAllStoredProcedures()
    {
        // Arrange
        const string destinationDatabaseName = "NorthwindDBCopy";
        
        var dbCopyOptions = new DbCopyOptions
        {
            CopyData = false,
            CopyStoredProcedures = true
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
            var getSourceTableColumnsSql = @"SELECT 
                                                m.definition AS ProcedureDefinition
                                            FROM 
                                                sys.procedures p
                                            JOIN 
                                                sys.sql_modules m ON p.object_id = m.object_id
                                            WHERE 
                                                p.type = 'P'
                                            ORDER BY 
                                                p.name;";
            
            var sourceSprocs = await sourceConnection.QueryAsync(getSourceTableColumnsSql);

            var destinationSprocs = await destinationConnection.QueryAsync(getSourceTableColumnsSql);
            
            destinationSprocs.Should().BeEquivalentTo(sourceSprocs);
        };
    }
    
}