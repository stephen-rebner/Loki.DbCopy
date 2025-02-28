using Loki.DbCopy.MsSqlServer.Commands;
using Loki.DbCopy.MsSqlServer.Commands.Interfaces;
using Loki.DbCopy.MsSqlServer.Context;
using Loki.DbCopy.MsSqlServer.DatabaseCopyFunctions;
using Loki.DbCopy.MsSqlServer.DatabaseCopyFunctions.Interfaces;
using Loki.DbCopy.MsSqlServer.Factories;
using Loki.MsSqlCopy.Common.Context;
using Loki.MsSqlDbCopy.Infrastructure.Repositories;
using Loki.MsSqlDbCopy.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Loki.DbCopy.MsSqlServer.DependencyInjection;

public static class MsSqlServerIocContainer
{
    // Add an extension method for IServiceCollection
    public static IServiceCollection AddMsSqlServerDbCopy(this IServiceCollection services)
    {
        // Register the IDbCopyContext and DbCopyContext classes
        services.AddSingleton<IDbCopyContext, DbCopyContext>();
        services.AddSingleton<IConnectionStringContext, ConnectionStringContext>();
        
        // Register the IDatabaseCopyCommand classes
        services.AddScoped<IDatabaseCopyCommand, DropDatabaseCommand>();
        services.AddScoped<IDatabaseCopyCommand, CreateDatabaseCommand>();
        services.AddScoped<IDatabaseCopyCommand, CopySchemasCommand>();
        services.AddScoped<IDatabaseCopyCommand, CopyTablesCommand>();
        // services.AddScoped<IDatabaseCopyCommand, CopyDataCommand>();
        services.AddScoped<IDatabaseCopyCommand, CopyPrimaryKeysCommand>();
        services.AddScoped<IDatabaseCopyCommand, CopyForeignKeysCommand>();
        services.AddScoped<IDatabaseCopyCommand, CopyIndexesCommand>();
        services.AddScoped<IDatabaseCopyCommand, CopyViewsCommand>();
        services.AddScoped<IDatabaseCopyCommand, CopyStoredProceduresCommand>();
        services.AddScoped<IDatabaseCopyCommand, CopyFunctionsCommand>();
        services.AddScoped<IDatabaseCopyCommand, CopyTriggersCommand>();
        
        // Register the IMsSqlTablesRepository and MsSqlTablesRepository classes
        services.AddScoped<IMsSqlTablesRepository, MsSqlTablesRepository>();

        services.AddScoped<IMsSqlSchemasRepository, MsSqlSchemasRepository>();

        services.AddScoped<IIndexesRepository, IndexesRepository>();
        
        // Register the Loki.MsSqlDbCopy.Infrastructure classes
        services.AddScoped<IMsSqlSchemasRepository, MsSqlSchemasRepository>();

        services.AddScoped<IStoredProceduresRepository, StoredProceduresRepository>();
        
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