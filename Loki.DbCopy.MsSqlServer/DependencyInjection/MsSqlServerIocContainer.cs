using Loki.DbCopy.Core;
using Loki.DbCopy.Core.Commands;
using Loki.DbCopy.Core.DbCopyOptions;
using Loki.DbCopy.Core.DependencyInjection;
using Loki.DbCopy.MsSqlServer.Commands;
using Loki.MsSqlDbCopy.Infrastructure;
using Loki.MsSqlDbCopy.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Loki.DbCopy.MsSqlServer.DependencyInjection;

public static class MsSqlServerIocContainer
{
    // Add an extension method for IServiceCollection
    public static IServiceCollection AddMsSqlServerDbCopy(this IServiceCollection services)
    {
        services.AddDbCopy();
        
        // Register the IDatabaseCopyCommand classes
        services.AddScoped<IDatabaseCopyCommand, DropDatabaseCommand>();
        services.AddScoped<IDatabaseCopyCommand, CreateDatabaseCommand>();
        services.AddScoped<IDatabaseCopyCommand, CreateSchemasCommand>();
        
        // Register the Loki.MsSqlDbCopy.Infrastructure classes
        services.AddScoped<IMsSqlSchemasRepository, MsSqlSchemasRepository>();
        
        // Register the MsSqlServerDatabaseCopier class
        services.AddScoped<IDatabaseCopier, MsSqlDbCopier>();

        // Return the IServiceCollection
        return services;
    }
}