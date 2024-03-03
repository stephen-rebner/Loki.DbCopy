using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Loki.DbCopy.MsSqlServer.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Loki.DbCopy.IntegrationTests.BaseIntegrationTests;

public class BaseMsSqlDbCopierIntegrationTests
{
    protected ServiceProvider ServiceProvider = null!;
    protected IContainer Container = null!;

    [SetUp]
    public async Task Setup()
    {
        SetupIocServiceProvider();

        await CreateNorthWindDbContainer();
    }
    
    [TearDown]
    public async Task TearDown()
    {
        await Container.DisposeAsync();
        
        ServiceProvider.Dispose();
    }
    
    private void SetupIocServiceProvider()
    {
        var services = new ServiceCollection()
            .AddMsSqlServerDbCopy();

        ServiceProvider = services.BuildServiceProvider();
    }
    
    private async Task CreateNorthWindDbContainer()
    {
        Container = new ContainerBuilder()
            .WithImage("stephenr1983/northwind-db-sqlserver:latest")
            .WithPortBinding(1433, 1433)
            .Build();

        await Container
            .StartAsync()
            .ConfigureAwait(false);
    }
}