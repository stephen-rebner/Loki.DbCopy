using System.Data.SqlClient;
using Dapper;
using FluentAssertions;
using Loki.DbCopy.IntegrationTests.BaseIntegrationTests;
using Loki.DbCopy.MsSqlServer;
using Loki.DbCopy.MsSqlServer.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Loki.DbCopy.IntegrationTests.Commands;

public class CopyFunctionsCommandTests : BaseMsSqlDbCopierIntegrationTests
{
    [Test]
    public async Task CopyFunctionsCommand_ShouldCopyAllFunctions()
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
    
        await using var sourceConnection = new SqlConnection(sourceConnectionStringBuilder.ToString());
        await sourceConnection.OpenAsync();
    
        // Create functions in the source database
        var createFunctionsSql = new[]
        {
            @"
                CREATE FUNCTION dbo.GetEmployeeFirstName (@FirstName NVARCHAR(50), @LastName NVARCHAR(50))
                RETURNS NVARCHAR(101)
                AS
                BEGIN
                    RETURN @FirstName + ' ' + @LastName;
                END;
            ",
            @"
                CREATE FUNCTION dbo.GetEmployeesByCountry (@country NVARCHAR(50))
                RETURNS TABLE
                AS
                RETURN
                (
                    SELECT EmployeeId, FirstName, LastName
                    FROM Employees
                    WHERE Country = @country
                );
            ",
            @"
                CREATE FUNCTION dbo.GetEmployeeDetails (@EmployeeId INT)
                RETURNS @EmployeeDetails TABLE
                (
                    EmployeeId INT,
                    FirstName NVARCHAR(50),
                    LastName NVARCHAR(50)
                )
                AS
                BEGIN
                    INSERT INTO @EmployeeDetails
                    SELECT e.EmployeeId, e.FirstName, e.LastName
                    FROM Employees e
                    WHERE e.EmployeeId = @EmployeeId;
    
                    RETURN;
                END;
            "
        };
    
        foreach (var sql in createFunctionsSql)
        {
            await sourceConnection.ExecuteAsync(sql);
        }
    
        var msSqlDbCopier = ServiceProvider.GetService<IMsSqlDbCopier>();
    
        // Act
        await msSqlDbCopier.Copy(sourceConnectionStringBuilder, destinationConnectionStringBuilder, dbCopyOptions);
    
        // Assert
        await using var destinationConnection = new SqlConnection(destinationConnectionStringBuilder.ToString());
        await destinationConnection.OpenAsync();
    
        var getFunctionsSql = @"
            SELECT sm.definition
            FROM sys.sql_modules sm
            INNER JOIN sys.objects o ON sm.object_id = o.object_id
            WHERE o.type IN ('FN', 'IF', 'TF')
            ORDER BY o.name;
        ";
    
        var sourceFunctions = await sourceConnection.QueryAsync<string>(getFunctionsSql);
        var destinationFunctions = await destinationConnection.QueryAsync<string>(getFunctionsSql);
    
        destinationFunctions.Should().BeEquivalentTo(sourceFunctions);
    }
}