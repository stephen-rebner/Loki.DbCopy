using Loki.DbCopy.Core.Context;
using Loki.DbCopy.Core.DbCopyOptions;
using Microsoft.Extensions.DependencyInjection;

namespace Loki.DbCopy.Core.DependencyInjection;

public static class DbCopyIocContainer
{
    // Create a new extension method for IServiceCollection
    //  this method will be used to register all of the classes in this project to the IoC container
    public static void AddDbCopy(this IServiceCollection services)
    {
        // Register the IDbCopyContext and DbCopyContext classes
        services.AddSingleton<IDbCopyContext, DbCopyContext>();
    }
}