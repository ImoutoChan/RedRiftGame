using NodaTime;

namespace RedRiftGame.Domain;

public class Match
{
    private Match(Guid id, Player host, MatchState matchState, Player? guest, Instant createdAt)
    {
        Id = id;
        Host = host;
        MatchState = matchState;
        Guest = guest;
        CreatedAt = createdAt;
    }

    public Guid Id { get; }

    public Player Host { get; }

    public MatchState MatchState { get; private set; }

    public Player? Guest { get; private set; }

    public int CurrentTurn { get; private set; }

    public Instant CreatedAt { get; }

    public Instant? FinishedAt { get; private set; }

    public bool IsFinished => MatchState is MatchState.Finished or MatchState.Interrupted;

    public bool IsHostWinner => Host.Health > 0;

    public Player GetGuest() => Guest ?? throw new Exception($"Guest for match {Id} is null");
    
    public static Match Create(string hostConnectionId, string hostName, Instant now)
        => new(Guid.NewGuid(), Player.Create(hostConnectionId, hostName), MatchState.Created, null, now);

    public void Join(Player guest)
    {
        if (MatchState != MatchState.Created)
            throw new Exception($"Match {Id} is in wrong state {MatchState}");

        Guest = guest;

        MatchState = MatchState.Running;
    }

    public void NextTurn(Instant now)
    {
        if (MatchState != MatchState.Running)
            throw new Exception($"Match {Id} is in wrong state {MatchState}");

        if (Guest == null)
            throw new Exception($"Guest for match {Id} is null");

        CurrentTurn++;

        Host.TakeBullet();
        Guest.TakeBullet();

        if (!Host.Alive || !Guest.Alive)
            FinishMatch(now);
    }

    public void Interrupt()
    {
        if (MatchState != MatchState.Created)
            throw new Exception($"Match {Id} is in wrong state {MatchState}");

        MatchState = MatchState.Interrupted;
    }

    private void FinishMatch(Instant now)
    {
        // todo change exceptions to typed exceptions
        if (MatchState != MatchState.Running)
            throw new Exception($"Match {Id} is in wrong state {MatchState}");

        MatchState = MatchState.Finished;

        FinishedAt = now;
    }
}
