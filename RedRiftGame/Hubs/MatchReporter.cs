using Microsoft.AspNetCore.SignalR;
using RedRiftGame.Application.Services;
using RedRiftGame.Domain;

namespace RedRiftGame.Hubs;

public class MatchReporter : IMatchReporter
{
    private readonly IHubContext<GameLobbyHub, IGameLobbyHubClient> _hubContext;

    public MatchReporter(IHubContext<GameLobbyHub, IGameLobbyHubClient> hubContext)
    {
        _hubContext = hubContext;
    }
    
    public async Task ReportAsync(Match match)
    {
        var host = match.Host;
        var guest = match.GetGuest();

        await _hubContext.Clients.Clients(host.ConnectionId, guest.ConnectionId)
            .ProcessGameFinished(match.Id, host.Name, guest.Name, host.Health, guest.Health, match.IsHostWinner);
    }
}