namespace Loki.DbCopy.Core.Commands;

public interface IDatabaseCopyCommand
{
    Task Execute();
}