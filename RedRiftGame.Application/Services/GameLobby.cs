using NodaTime;
using RedRiftGame.Domain;

namespace RedRiftGame.Application.Services;

// singleton
internal class GameLobby : IGameLobby
{
    private readonly List<Match> _currentMatches;

    public GameLobby() => _currentMatches = new List<Match>();

    public void CreateMatch(Match match)
    {
        if (_currentMatches.Any(x => x.Host.ConnectionId != match.Host.ConnectionId))
            throw new MatchHandlingException("Player already has room");

        _currentMatches.Add(match);
    }

    public Match JoinMatch(Guid id, Player guest)
    {
        var match = _currentMatches.FirstOrDefault(x => x.Id == id);

        if (match == null)
            throw new MatchHandlingException("Match can't be found");

        match.Join(guest);

        return match;
    }

    public void RunMatches(Instant now)
    {
        var runningMatches = _currentMatches.Where(x => x.MatchState == MatchState.Running);

        foreach (var runningMatch in runningMatches) 
            runningMatch.NextTurn(now);
    }

    public void InterruptMatch(string hostConnectionId)
        => _currentMatches
            .FirstOrDefault(x => x.Host.ConnectionId == hostConnectionId && x.MatchState == MatchState.Created)
            ?.Interrupt();

    public IReadOnlyCollection<Match> RemoveFinishedMatches()
    {
        var finishedMatches = _currentMatches.Where(x => x.MatchState == MatchState.Finished).ToList();
        var finishedMatchesIds = finishedMatches.Select(x => x.Id).ToHashSet();
        _currentMatches.RemoveAll(x => finishedMatchesIds.Contains(x.Id));

        return finishedMatches;
    }
}
