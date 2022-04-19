using MediatR;
using RedRiftGame.Application.Services;
using RedRiftGame.Common.Cqs;
using RedRiftGame.Domain;

namespace RedRiftGame.Application.Cqs;

internal class JoinRoomCommandHandler : ICommandHandler<JoinRoom>
{
    private readonly IGameLobby _gameLobby;

    public JoinRoomCommandHandler(IGameLobby gameLobby) => _gameLobby = gameLobby;

    public Task<Unit> Handle(JoinRoom request, CancellationToken cancellationToken)
    {
        var (roomId, connectionId, name) = request;

        var guestPlayer = Player.Create(connectionId, name);
        _gameLobby.JoinMatch(roomId, guestPlayer);

        return Task.FromResult(Unit.Value);
    }
}