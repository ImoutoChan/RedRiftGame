using MediatR;
using Microsoft.AspNetCore.SignalR;
using RedRiftGame.Application.Cqs;

namespace RedRiftGame.Hubs;

public class GameLobbyHub : Hub<IGameLobbyHubClient>
{
    private readonly IMediator _mediator;

    public GameLobbyHub(IMediator mediator) => _mediator = mediator;

    public async Task CreateMatch(string hostName)
    {
        var matchId = await _mediator.Send(new CreateMatch(Context.ConnectionId, hostName));
        await Clients.Others.ProcessNewMatch(matchId, hostName);
    }

    public async Task JoinMatch(Guid id, string guestName)
    {
        var match = await _mediator.Send(new JoinMatch(id, Context.ConnectionId, guestName));

        var host = match.Host;
        
        await Clients
            .Clients(host.ConnectionId, match.GetGuest().ConnectionId)
            .ProcessGameStarted(id, host.Name, guestName);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await _mediator.Send(new InterruptMatch(Context.ConnectionId));
        await base.OnDisconnectedAsync(exception); 
    }
}
