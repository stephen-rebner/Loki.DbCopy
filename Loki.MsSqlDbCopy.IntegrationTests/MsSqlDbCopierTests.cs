using Loki.DbCopy.Core;
using Loki.DbCopy.Core.DbCopyOptions;
using Loki.DbCopy.IntegrationTests.BaseIntegrationTests;
using Microsoft.Extensions.DependencyInjection;

namespace Loki.DbCopy.IntegrationTests;

public class MsSqlDbCopierTests : BaseMsSqlDbCopierIntegrationTests
{
    [Test]
    public async Task Copy_ShouldCopyAllDataFromAllDbTables()
    {
        // Arrange
        var dbCopier = ServiceProvider.GetRequiredService<IDatabaseCopier>();
        
        // Act
        await dbCopier.Copy("Server=localhost,1433;Database=Northwind;User Id=sa;Password=Password123", "Server=localhost,1433;Database=NorthwindCopy;User Id=sa;Password=Password123");
        
        Assert.IsTrue(false);
    }
}