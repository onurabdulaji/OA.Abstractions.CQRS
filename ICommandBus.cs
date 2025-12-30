using OA.Result.Results;

namespace OA.Abstractions.CQRS;

using Result = Result.Results.Result;

public interface ICommandBus
{
    Task<Result> SendAsync(ICommand command, CancellationToken ct = default);

    Task<Result<T>> SendAsync<T>(ICommand<Result<T>> command, CancellationToken ct = default);
}