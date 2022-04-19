using MediatR;

namespace RedRiftGame.Common.Cqs;

public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
{
}