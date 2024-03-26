using Loki.DbCopy.MsSqlServer.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace Loki.DbCopy.IntegrationTests.BaseIntegrationTests;

public abstract class BaseMsSqlDbCopierIntegrationTests
{
    protected ServiceProvider ServiceProvider = null!;
    
    protected MsSqlContainer SourceNorthWindDbContainer = null!;
    
    protected MsSqlContainer DestinationNorthWindDbContainer = null!;

    protected const string UserId = "sa";

    protected const string Password = "Password123";


    [SetUp]
    public async Task Setup()
    {
        SetupIocServiceProvider();

        await CreateSourceNorthWindDbContainer();
        
        await CreateDestinationNorthWindDbContainer();
    }
    
    [TearDown]
    public async Task TearDown()
    {
        await SourceNorthWindDbContainer.DisposeAsync();
        
        await DestinationNorthWindDbContainer.DisposeAsync();
        
        ServiceProvider.Dispose();
    }
    
    private void SetupIocServiceProvider()
    {
        var services = new ServiceCollection().AddMsSqlServerDbCopy();

        ServiceProvider = services.BuildServiceProvider();
    }
    
    private async Task CreateDestinationNorthWindDbContainer()
    {
        // create a sql server container
        DestinationNorthWindDbContainer = new MsSqlBuilder()
            .WithPassword(Password)
            // .WithPortBinding(52774, 1433)
            .WithWorkingDirectory("/var/opt/mssql")
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .Build();
        
        await DestinationNorthWindDbContainer.StartAsync();
    }

    private async Task CreateSourceNorthWindDbContainer()
    {
        // create a sql server container
        SourceNorthWindDbContainer = new MsSqlBuilder()
            .WithPassword(Password)
            // .WithPortBinding(1433, 1433)
            .WithWorkingDirectory("/var/opt/mssql")
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .Build();
        
        await SourceNorthWindDbContainer.StartAsync();

        // copy the northwind database backup file to the container
        var northwindDbBackupFile = new FileInfo("./NorthwindDatabaseBackup/Northwind.bak");
        
        await SourceNorthWindDbContainer.CopyAsync(northwindDbBackupFile, "/var/opt/mssql/data/");
    }
}