using MediatR;

namespace RedRiftGame.Common;

public interface IQuery<out T> : IRequest<T>
{
}

public interface ICommand : IRequest
{
}

public interface ICommand<out T> : IRequest<T>
{
}

public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
{
}

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand> where TCommand : ICommand
{
}

public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
}
