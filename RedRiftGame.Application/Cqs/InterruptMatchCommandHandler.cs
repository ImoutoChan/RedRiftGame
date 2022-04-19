using MediatR;
using RedRiftGame.Application.Services;
using RedRiftGame.Common.Cqs;

namespace RedRiftGame.Application.Cqs;

internal class InterruptMatchCommandHandler : ICommandHandler<InterruptMatch>
{
    private readonly IGameLobby _gameLobby;

    public InterruptMatchCommandHandler(IGameLobby gameLobby) => _gameLobby = gameLobby;

    public Task<Unit> Handle(InterruptMatch request, CancellationToken cancellationToken)
    {
        _gameLobby.InterruptMatch(request.ConnectionId);
        
        return Task.FromResult(Unit.Value);
    }
}