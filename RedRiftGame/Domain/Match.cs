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

    public Player? Guest { get; }

    public static Match Create(string playerConnectionId, string playerName) 
        => new(Guid.NewGuid(), Player.Create(playerConnectionId, playerName), MatchState.Created, null);

    public void Start()
    {
        if (Guest == null)
            throw new Exception($"Guest for match {Id} is null");
        
        if (MatchState != MatchState.Created)
            throw new Exception($"Match {Id} is in wrong state {MatchState}");

        MatchState = MatchState.Running;
    }
    
    public void NextTurn()
    {
        if (MatchState != MatchState.Running)
            throw new Exception($"Match {Id} is in wrong state {MatchState}");
        
        if (Guest == null)
            throw new Exception($"Guest for match {Id} is null");
        
        Host.TakeBullet();
        Guest.TakeBullet();

        if (!Host.Alive || Guest.Alive)
            FinishMatch();
    }

    public void Interrupt()
    {
        MatchState = MatchState.Interrupted;
    }

    private void FinishMatch()
    {
        if (MatchState != MatchState.Running)
            throw new Exception($"Match {Id} is in wrong state {MatchState}");
        
        MatchState = MatchState.Finished;
    }
}