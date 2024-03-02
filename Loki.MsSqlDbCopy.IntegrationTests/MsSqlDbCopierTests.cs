using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace Loki.DbCopy.IntegrationTests;

public class MsSqlDbCopierTests
{
    private IContainer _container = null!;


    [SetUp]
    public async Task SetUp()
    {
        _container = new ContainerBuilder()
            .WithImage("stephenr1983/northwind-db-sqlserver:latest")
            .WithPortBinding(1433, 1433)
            .Build();

        await _container
            .StartAsync()
            .ConfigureAwait(false);
    }

    [TearDown]
    public async Task TearDown()
    {
        await _container
            .StopAsync()
            .ConfigureAwait(false);
    }


    [Test]
    public void Copy_ShouldCopyAllDataFromAllDbTables()
    {
        Assert.IsTrue(false);
    }
}