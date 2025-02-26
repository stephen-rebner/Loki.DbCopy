using Loki.DbCopy.MsSqlServer.Commands.Interfaces;
using Loki.DbCopy.MsSqlServer.Context;
using Loki.MsSqlCopy.Common.Context;
using Loki.MsSqlDbCopy.Infrastructure.Repositories.Interfaces;

namespace Loki.DbCopy.MsSqlServer.Commands;

/// <summary>
/// MsSqlServer command responsible for copying the indexes from the source database.
/// </summary>
internal class CopyIndexesCommand(IDbCopyContext dbCopyContext, IConnectionStringContext connectionStringContext, IIndexesRepository msSqlIndexesRepository) 
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
        
        var indexNames = await msSqlIndexesRepository.LoadIndexes(connectionStringContext.SourceConnectionString);

        if(indexNames.Length > 0)
        {
            await msSqlIndexesRepository.CreateIndexes(connectionStringContext.DestinationConnectionString);
        }
    }
}