using MediatR;

namespace RedRiftGame.Common.Cqs;

public interface ICommand : IRequest
{
}

public interface ICommand<out T> : IRequest<T>
{
}