using MediatR;
using NodaTime;
using RedRiftGame.Application.Services;
using RedRiftGame.Common.Cqs;
using RedRiftGame.Domain;

namespace RedRiftGame.Application.Cqs;

internal class CreateRoomCommandHandler : ICommandHandler<CreateMatch, Guid>
{
    private readonly IClock _clock;
    private readonly IGameLobby _gameLobby;

    public CreateRoomCommandHandler(IGameLobby gameLobby, IClock clock)
    {
        _gameLobby = gameLobby;
        _clock = clock;
    }

    public Task<Guid> Handle(CreateMatch request, CancellationToken cancellationToken)
    {
        var (connectionId, name) = request;
        var now = _clock.GetCurrentInstant();

        var newMatch = Match.Create(connectionId, name, now);
        _gameLobby.CreateMatch(newMatch);

        return Task.FromResult(newMatch.Id);
    }
}
