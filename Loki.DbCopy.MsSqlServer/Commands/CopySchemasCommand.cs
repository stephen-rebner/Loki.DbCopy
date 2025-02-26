using Loki.DbCopy.MsSqlServer.Commands.Interfaces;
using Loki.DbCopy.MsSqlServer.Context;
using Loki.MsSqlCopy.Common.Context;
using Loki.MsSqlDbCopy.Infrastructure.Repositories.Interfaces;

namespace Loki.DbCopy.MsSqlServer.Commands;

/// <summary>
/// MsSqlServer command responsible for dropping the destination database if it exists.
/// </summary>
internal class CopySchemasCommand(IDbCopyContext dbCopyContext, IMsSqlSchemasRepository msSqlSchemasRepository) 
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
        
        var schemaNames = await msSqlSchemasRepository.LoadSchemas();

        if (schemaNames.Length > 0)
        {
            await msSqlSchemasRepository.CreateSchemas(schemaNames);
        }
    }
}