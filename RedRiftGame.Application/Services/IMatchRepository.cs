using RedRiftGame.Domain;

namespace RedRiftGame.Application.Services;

public interface IMatchRepository
{
    Task AppendAsync(Match match);
}