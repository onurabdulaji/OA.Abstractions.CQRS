using OA.Result.Results;

namespace OA.Abstractions.CQRS;

public interface IQueryBus
{
    Task<Result<T>> QueryAsync<T>(IQuery<Result<T>> query, CancellationToken ct = default);
}