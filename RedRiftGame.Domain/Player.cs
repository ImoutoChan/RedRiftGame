namespace RedRiftGame.Domain;

public class Player
{
    private const int DefaultHealth = 10;
    private const int MaxDamage = 2;

    private Player(string connectionId, string name, int health)
    {
        ConnectionId = connectionId;
        Name = name;
        Health = health;
    }

    public string ConnectionId { get; }

    public string Name { get; }

    public int Health { get; private set; }

    public bool Alive => Health > 0;

    public static Player Create(string connectionId, string name) => new(connectionId, name, DefaultHealth);

    public void TakeBullet() => Health -= Random.Shared.Next(MaxDamage);
}