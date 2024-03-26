﻿using Loki.DbCopy.Core.Context;
using Loki.DbCopy.MsSqlServer.Commands.Interfaces;
using Loki.MsSqlDbCopy.Infrastructure.Interfaces;

namespace Loki.DbCopy.MsSqlServer.Commands;

/// <summary>
/// MsSqlServer command responsible for dropping the destination database if it exists.
/// </summary>
public class CreateSchemasCommand(IDbCopyContext dbCopyContext, IMsSqlSchemasRepository msSqlSchemasRepository) 
    : IDatabaseCopyCommand
{
    /// <summary>
    /// Copy the schemas if the CreateSchemas option is set to true.
    /// Otherwise, this step is skipped.
    /// </summary>
    public async Task Execute()
    {
        if(dbCopyContext.DbCopyOptions.CreateSchemas == false)
        {
            return;
        }
        
        var schemaNames = await msSqlSchemasRepository.LoadSchemas(dbCopyContext.SourceConnectionString);

        if (schemaNames.Length > 0)
        {
            await msSqlSchemasRepository.CreateSchemas(dbCopyContext.DestinationConnectionString, schemaNames);
        }
    }
}