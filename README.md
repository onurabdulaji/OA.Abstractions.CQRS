# OA.Abstractions.CQRS

CQRS abstractions for .NET — lightweight interfaces and patterns to implement Command/Query Responsibility Segregation (
CQRS) in your application. This library provides small, focused interfaces that can be used directly or adapted to
popular libraries such as MediatR and Wolverine.

## Purpose

This package contains only the core abstractions (commands, queries, handlers and pipeline behaviors). It intentionally
keeps implementation out of the package so you can adapt these interfaces to your preferred messaging/mediator
framework.

Typical use-cases:

- Define commands/queries and their handlers with simple, testable interfaces
- Plug these abstractions into MediatR or Wolverine with small adapter classes
- Compose pipeline behaviors (cross-cutting concerns) such as logging, validation, or transactions

## Installation

Assumption: the NuGet package ID matches the project name `OA.Abstractions.CQRS`. If your package ID is different, use
that instead.

- .NET CLI

  dotnet add package OA.Abstractions.CQRS

- Package Manager Console

  Install-Package OA.Abstractions.CQRS

## Key interfaces (overview)

- `ICommand` / `ICommand<TResponse>`
- `IQuery<TResponse>`
- `ICommandHandler<TCommand>` / `ICommandHandler<TCommand, TResponse>`
- `IQueryHandler<TQuery, TResponse>`
- `ICommandBus` / `IQueryBus`
- `IPipelineBehavior<TRequest, TResponse>`

These are small, single-responsibility interfaces designed to express intent and make testing straightforward.

## Quick examples

Below are concise examples showing how you might adapt these abstractions to MediatR and Wolverine. These snippets are
illustrative — adapt to your project's structure and DI setup.

- Example command and handler (using the library interfaces):

  // Define a command
  public record CreateUserCommand(string Name, string Email) : ICommand<Guid>;

  // Implement a handler
  public class CreateUserHandler : ICommandHandler<CreateUserCommand, Guid>
  {
  public Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
  {
  // Implementation: create user, return id
  return Task.FromResult(Guid.NewGuid());
  }
  }

- Adapting to MediatR

A small adapter can map `ICommand<T>` to `MediatR.IRequest<T>` and forward calls to your
`ICommandHandler<TCommand, TResponse>` implementations registered in DI. Alternatively, implement MediatR handlers that
internally call the abstractions in this package.

- Using with Wolverine

Wolverine uses messages and handlers. Treat `ICommand` implementations as Wolverine messages and register handler types
that implement the `ICommandHandler<TCommand, TResponse>` interface. You can also write a thin adapter that forwards
Wolverine messages to the abstraction's handlers, enabling reuse of the same handler logic.

## Pipeline behaviors

Implement `IPipelineBehavior<TRequest, TResponse>` for cross-cutting concerns (logging, validation, retries). Register
behaviors in your DI container in the order you want them applied.

## Testing

Because these are interfaces with no framework dependencies, they are very easy to unit test — mock the handler
interfaces to verify interactions or test handler classes directly.

## Contributing

Contributions and suggestions welcome. Keep the core package focused on abstractions only; any concrete adapters for
MediatR, Wolverine or other infra are better maintained in separate packages.

## License

This project is licensed under the terms in the `LICENSE` file included in the repository.
