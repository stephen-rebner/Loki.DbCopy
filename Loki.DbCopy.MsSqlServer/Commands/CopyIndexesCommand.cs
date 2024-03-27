using Loki.DbCopy.Core.Context;
using Loki.DbCopy.MsSqlServer.Commands.Interfaces;
using Loki.MsSqlDbCopy.Infrastructure.Interfaces;

namespace Loki.DbCopy.MsSqlServer.Commands;

/// <summary>
/// MsSqlServer command responsible for copying the indexes from the source database.
/// </summary>
internal class CopyIndexesCommand(IDbCopyContext dbCopyContext, IIndexesRepository msSqlIndexesRepository) 
    : IDatabaseCopyCommand
{
    /// <summary>
    /// Copies the indexes if the CopyIndexes option is set to true.
    /// Otherwise, this step is skipped.
    /// </summary>
    public async Task Execute()
    {
        if(dbCopyContext.DbCopyOptions.CopyIndexes == false)
        {
            return;
        }
        
        var indexNames = await msSqlIndexesRepository.LoadIndexes(dbCopyContext.SourceConnectionString);

        if(indexNames.Length > 0)
        {
            await msSqlIndexesRepository.CreateIndexes(dbCopyContext.DestinationConnectionString);
        }
    }
}