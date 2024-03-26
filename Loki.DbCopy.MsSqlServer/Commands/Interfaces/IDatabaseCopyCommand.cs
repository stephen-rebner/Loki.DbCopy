namespace Loki.DbCopy.MsSqlServer.Commands.Interfaces;

public interface IDatabaseCopyCommand
{
    Task Execute();
}