using RedRiftGame.Domain;

namespace RedRiftGame.Application;

public class GameLobby : IGameLobby, IGameLobbyRunner
{
    private readonly List<Match> _currentMatches;
    private readonly PeriodicTimer _periodicTimer;
    
    public GameLobby()
    {
        _currentMatches = new List<Match>();
        _periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(1));
    }
    
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
    {
        CurrentMatches.FirstOrDefault(x => x.Host.ConnectionId == hostConnectionId)?.Interrupt();
    }

    public async Task RunMatchesAsync()
    {
        while (await _periodicTimer.WaitForNextTickAsync())
        {
            var runningMatches = _currentMatches.Where(x => x.MatchState == MatchState.Running);

            foreach (var runningMatch in runningMatches)
            {
                runningMatch.NextTurn();
            }
        }
        
        var finishedMatches = _currentMatches.Where(x => x.MatchState == MatchState.Finished).ToList();
        var finishedMatchesIds = finishedMatches.Select(x => x.Id).ToHashSet();

        _currentMatches.RemoveAll(x => finishedMatchesIds.Contains(x.Id));
    }
}
