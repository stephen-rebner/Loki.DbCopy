using Loki.DbCopy.Core;
using Loki.DbCopy.Core.Commands;
using Loki.DbCopy.Core.DbCopyOptions;
using Loki.DbCopy.Core.DependencyInjection;
using Loki.DbCopy.MsSqlServer.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Loki.DbCopy.MsSqlServer.DependencyInjection;

public static class MsSqlServerIocContainer
{
    // Add an extension method for IServiceCollection
    public static IServiceCollection AddMsSqlServerDbCopy(this IServiceCollection services)
    {
        services.AddDbCopy();
        
        // Register the IDatabaseCopyCommand classes
        services.AddScoped<IDatabaseCopyCommand, DropDatabaseIfExistsCommand>();
        
        // Register the MsSqlServerDatabaseCopier class
        services.AddScoped<IDatabaseCopier, MsSqlDbCopier>();

        // Return the IServiceCollection
        return services;
    }
}