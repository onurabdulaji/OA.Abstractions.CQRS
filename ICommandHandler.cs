namespace OA.Abstractions.CQRS;

using Result = Result.Results.Result;

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    Task<Result> Handle(TCommand command, CancellationToken ct = default);
}