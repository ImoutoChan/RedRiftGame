using MediatR;
using RedRiftGame.Common;
using RedRiftGame.Domain;

namespace RedRiftGame.Application.Cqs;

internal class JoinRoomCommandHandler : ICommandHandler<JoinRoom>
{
    private readonly IGameLobby _gameLobby;

    public JoinRoomCommandHandler(IGameLobby gameLobby) => _gameLobby = gameLobby;

    public Task<Unit> Handle(JoinRoom request, CancellationToken cancellationToken)
    {
        var guestPlayer = Player.Create(request.ConnectionId, request.Name);
        
        _gameLobby.JoinMatch(request.RoomId, guestPlayer);
        
        return Task.FromResult(Unit.Value);
    }
}