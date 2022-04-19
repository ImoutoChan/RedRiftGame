using RedRiftGame.Domain;

namespace RedRiftGame.Application.Services;

internal interface IGameLobby
{
    IReadOnlyCollection<Match> CurrentMatches { get; }

    void CreateMatch(Match match);

    void JoinMatch(Guid id, Player guest);

    void InterruptMatch(string hostConnectionId);

    void RemoveMatches(IReadOnlyCollection<Guid> finishedMatchesIds);
}