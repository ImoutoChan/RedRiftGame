using NodaTime;
using RedRiftGame.Domain;

namespace RedRiftGame.Application.Services;

internal interface IGameLobby
{
    void CreateMatch(Match match);

    Match JoinMatch(Guid id, Player guest);

    void InterruptMatch(string hostConnectionId);
    
    void RunMatches(Instant now);

    IReadOnlyCollection<Match> RemoveFinishedMatches();
}
