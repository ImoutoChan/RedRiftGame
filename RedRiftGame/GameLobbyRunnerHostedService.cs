using RedRiftGame.Application.Services;

namespace RedRiftGame;

internal class GameLobbyRunnerHostedService : IHostedService
{
    private readonly IGameLobbyRunner _gameLobbyRunner;
    private readonly CancellationTokenSource _cts;

    public GameLobbyRunnerHostedService(IGameLobbyRunner gameLobbyRunner)
    {
        _gameLobbyRunner = gameLobbyRunner;
        _cts = new CancellationTokenSource();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(async () => await _gameLobbyRunner.RunMatchesAsync(_cts.Token), cancellationToken);
        Task.Run(async () => await _gameLobbyRunner.ReportMatchesAsync(_cts.Token), cancellationToken);
        
        return Task.FromResult(Task.CompletedTask);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _cts.Cancel();

        return Task.CompletedTask;
    }
}
