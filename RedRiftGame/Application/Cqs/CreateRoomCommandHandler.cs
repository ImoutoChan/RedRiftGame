using MediatR;
using RedRiftGame.Common;
using RedRiftGame.Domain;

namespace RedRiftGame.Application.Cqs;

internal class CreateRoomCommandHandler : ICommandHandler<CreateRoom>
{
    private readonly IGameLobby _gameLobby;

    public CreateRoomCommandHandler(IGameLobby gameLobby) => _gameLobby = gameLobby;

    public Task<Unit> Handle(CreateRoom request, CancellationToken cancellationToken)
    {
        var newMatch = Match.Create(request.ConnectionId, request.Name);
        
        _gameLobby.CreateMatch(newMatch);
        
        return Task.FromResult(Unit.Value);
    }
}