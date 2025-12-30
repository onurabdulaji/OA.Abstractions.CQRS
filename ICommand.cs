namespace OA.Abstractions.CQRS;

using Result = Result.Results.Result;

public interface ICommand : ICommand<Result>
{
}