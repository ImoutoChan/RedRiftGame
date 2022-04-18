namespace RedRiftGame.Domain;

public class Match
{
    private Match(Guid id, Player host, MatchState matchState, Player? guest)
    {
        Id = id;
        Host = host;
        MatchState = matchState;
        Guest = guest;
    }

    public Guid Id { get; }
    
    public Player Host { get; }
    
    public MatchState MatchState { get; private set; }

    public Player? Guest { get; private set; }
    
    public int CurrentTurn { get; private set; }

    public bool IsFinished => MatchState is MatchState.Finished or MatchState.Interrupted;

    public static Match Create(string hostConnectionId, string hostName) 
        => new(Guid.NewGuid(), Player.Create(hostConnectionId, hostName), MatchState.Created, null);

    public void Join(Player guest)
    {
        if (MatchState != MatchState.Created)
            throw new Exception($"Match {Id} is in wrong state {MatchState}");
        
        Guest = guest;

        MatchState = MatchState.Running;
    }
    
    public void NextTurn()
    {
        if (MatchState != MatchState.Running)
            throw new Exception($"Match {Id} is in wrong state {MatchState}");
        
        if (Guest == null)
            throw new Exception($"Guest for match {Id} is null");

        CurrentTurn++;
        
        Host.TakeBullet();
        Guest.TakeBullet();

        if (!Host.Alive || !Guest.Alive)
            FinishMatch();
    }

    public void Interrupt()
    {
        if (MatchState != MatchState.Created)
            throw new Exception($"Match {Id} is in wrong state {MatchState}");

        MatchState = MatchState.Interrupted;
    }

    private void FinishMatch()
    {
        if (MatchState != MatchState.Running)
            throw new Exception($"Match {Id} is in wrong state {MatchState}");
        
        MatchState = MatchState.Finished;
    }
}
