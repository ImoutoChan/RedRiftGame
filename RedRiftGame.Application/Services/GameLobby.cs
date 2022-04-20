using System.Collections.Concurrent;
using NodaTime;
using RedRiftGame.Domain;

namespace RedRiftGame.Application.Services;

// singleton
internal class GameLobby : IGameLobby
{
    private readonly ConcurrentDictionary<Guid, Match> _currentMatches;

    public GameLobby() => _currentMatches = new ConcurrentDictionary<Guid, Match>();

    public void CreateMatch(Match match)
    {
        if (_currentMatches.Any(x => x.Value.Host.ConnectionId == match.Host.ConnectionId))
            throw new MatchHandlingException("Player already has room");

        _currentMatches.TryAdd(match.Id, match);
    }

    public Match JoinMatch(Guid id, Player guest)
    {
        if (!_currentMatches.TryGetValue(id, out var match))
            throw new MatchHandlingException("Match can't be found");

        lock (match)
            match.Join(guest);

        return match;
    }

    public void RunMatches(Instant now)
    {
        var runningMatches = _currentMatches.Where(x => x.Value.MatchState == MatchState.Running);

        foreach (var runningMatch in runningMatches)
            runningMatch.Value.NextTurn(now);
    }

    public void InterruptMatch(string hostConnectionId)
    {
        var match = _currentMatches
            .FirstOrDefault(x => x.Value.Host.ConnectionId == hostConnectionId
                                 && x.Value.MatchState == MatchState.Created)
            .Value;

        if (match == null) 
            return;
        
        lock (match)
            match.Interrupt();

        RemoveFinishedMatches();
    }

    public IReadOnlyCollection<Match> RemoveFinishedMatches()
    {
        var finishedMatches = _currentMatches.Where(x => x.Value.IsFinished).ToList();

        foreach (var finishedMatch in finishedMatches) 
            _currentMatches.TryRemove(finishedMatch.Key, out _);

        return finishedMatches.Select(x => x.Value).Where(x => x.MatchState == MatchState.Finished).ToList();
    }
}
