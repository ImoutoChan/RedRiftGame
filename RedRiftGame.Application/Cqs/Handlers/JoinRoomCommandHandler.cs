using RedRiftGame.Application.Services;
using RedRiftGame.Common.Cqs;
using RedRiftGame.Domain;

namespace RedRiftGame.Application.Cqs.Handlers;

internal class JoinRoomCommandHandler : ICommandHandler<JoinMatch, Match>
{
    private readonly IGameLobby _gameLobby;

    public JoinRoomCommandHandler(IGameLobby gameLobby) => _gameLobby = gameLobby;

    public Task<Match> Handle(JoinMatch request, CancellationToken cancellationToken)
    {
        var (roomId, connectionId, name) = request;

        var guestPlayer = Player.Create(connectionId, name);
        var match = _gameLobby.JoinMatch(roomId, guestPlayer);

        return Task.FromResult(match);
    }
}
