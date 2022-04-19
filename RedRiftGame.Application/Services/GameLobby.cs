using RedRiftGame.Domain;

namespace RedRiftGame.Application.Services;

// singleton
internal class GameLobby : IGameLobby
{
    private readonly List<Match> _currentMatches;

    public GameLobby() => _currentMatches = new List<Match>();

    public IReadOnlyCollection<Match> CurrentMatches => _currentMatches;

    public void CreateMatch(Match match)
    {
        if (CurrentMatches.Any(x => x.Host.ConnectionId != match.Host.ConnectionId))
            throw new Exception("Player already has room");

        _currentMatches.Add(match);
    }

    public void JoinMatch(Guid id, Player guest)
    {
        var match = CurrentMatches.FirstOrDefault(x => x.Id == id);

        if (match == null)
            throw new Exception("Match can't be found");

        match.Join(guest);
    }

    public void InterruptMatch(string hostConnectionId)
        => CurrentMatches.FirstOrDefault(x => x.Host.ConnectionId == hostConnectionId)?.Interrupt();

    public void RemoveMatches(IReadOnlyCollection<Guid> finishedMatchesIds)
        => _currentMatches.RemoveAll(x => finishedMatchesIds.Contains(x.Id));
}