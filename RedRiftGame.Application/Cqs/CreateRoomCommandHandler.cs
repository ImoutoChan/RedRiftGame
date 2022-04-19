using MediatR;
using NodaTime;
using RedRiftGame.Application.Services;
using RedRiftGame.Common.Cqs;
using RedRiftGame.Domain;

namespace RedRiftGame.Application.Cqs;

internal class CreateRoomCommandHandler : ICommandHandler<CreateRoom>
{
    private readonly IGameLobby _gameLobby;
    private readonly IClock _clock;

    public CreateRoomCommandHandler(IGameLobby gameLobby, IClock clock)
    {
        _gameLobby = gameLobby;
        _clock = clock;
    }

    public Task<Unit> Handle(CreateRoom request, CancellationToken cancellationToken)
    {
        var (connectionId, name) = request;
        var now = _clock.GetCurrentInstant();
        
        var newMatch = Match.Create(connectionId, name, now);
        _gameLobby.CreateMatch(newMatch);
        
        return Task.FromResult(Unit.Value);
    }
}
