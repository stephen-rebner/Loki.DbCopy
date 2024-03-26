﻿using Loki.DbCopy.Core.Context;
using Loki.DbCopy.MsSqlServer.Commands;
using Loki.DbCopy.MsSqlServer.Commands.Interfaces;
using Loki.DbCopy.MsSqlServer.Context;
using Loki.DbCopy.MsSqlServer.DatabaseCopyFunctions;
using Loki.DbCopy.MsSqlServer.DatabaseCopyFunctions.Interfaces;
using Loki.DbCopy.MsSqlServer.Factories;
using Loki.MsSqlDbCopy.Infrastructure;
using Loki.MsSqlDbCopy.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Loki.DbCopy.MsSqlServer.DependencyInjection;

public static class MsSqlServerIocContainer
{
    // Add an extension method for IServiceCollection
    public static IServiceCollection AddMsSqlServerDbCopy(this IServiceCollection services)
    {
        // Register the IDbCopyContext and DbCopyContext classes
        services.AddSingleton<IDbCopyContext, DbCopyContext>();
        
        // Register the IDatabaseCopyCommand classes
        services.AddScoped<IDatabaseCopyCommand, DropDatabaseCommand>();
        services.AddScoped<IDatabaseCopyCommand, CreateDatabaseCommand>();
        services.AddScoped<IDatabaseCopyCommand, CopySchemasCommand>();
        
        // Register the Loki.MsSqlDbCopy.Infrastructure classes
        services.AddScoped<IMsSqlSchemasRepository, MsSqlSchemasRepository>();
        
        // Register the IMsSqlDbStructureCopier and MsSqlDbStructureCopier classes
        services.AddScoped<IMsSqlDbStructureCopier, MsSqlDbStructureCopier>();
        
        // Register the IMsSqlDbDataCopier and MsSqlDbDataCopier classes
        services.AddScoped<IMsSqlDbDataCopier, MsSqlDbDataCopier>();
        
        // Resister the IDbCopyCommandFactory class
        services.AddScoped<IMsSqlDbCopyCommandFactory, MsSqlDbCopyCommandFactory>();
        
        // Register the MsSqlServerDatabaseCopier class
        services.AddScoped<IMsSqlDbCopier, MsSqlDbCopier>();

        // Return the IServiceCollection
        return services;
    }
}