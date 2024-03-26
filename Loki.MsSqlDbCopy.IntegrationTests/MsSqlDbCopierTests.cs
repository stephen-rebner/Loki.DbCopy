using Loki.DbCopy.Core;
using Loki.DbCopy.IntegrationTests.BaseIntegrationTests;
using Microsoft.Extensions.DependencyInjection;

namespace Loki.DbCopy.IntegrationTests;

public class MsSqlDbCopierTests : BaseMsSqlDbCopierIntegrationTests
{
    [Ignore("Not implemented yet")]
    [Test]
    public async Task Copy_ShouldCopyAllDataFromAllDbTables()
    {
        // Arrange
        var dbCopier = ServiceProvider.GetRequiredService<IDatabaseCopier>();
        
        // Act
        await dbCopier.Copy("Server=localhost,1433;NorthwindDatabaseBackup=Northwind;User Id=sa;Password=Password123", "Server=localhost,1433;NorthwindDatabaseBackup=NorthwindCopy;User Id=sa;Password=Password123");
        
        Assert.IsTrue(false);
    }
    
    
}