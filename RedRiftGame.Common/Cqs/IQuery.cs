using MediatR;

namespace RedRiftGame.Common.Cqs;

public interface IQuery<out T> : IRequest<T>
{
}