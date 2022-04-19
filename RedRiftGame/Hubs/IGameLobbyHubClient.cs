namespace RedRiftGame.Hubs;

public interface IGameLobbyHubClient
{
    Task ProcessNewMatch(Guid matchId, string hostName);

    Task ProcessGameStarted(Guid matchId, string hostName, string guestName);

    Task ProcessGameFinished(
        Guid matchId,
        string hostName,
        string guestName,
        int hostHealth,
        int guestHealth,
        bool isHostWinner);
}
