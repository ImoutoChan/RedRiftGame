using RedRiftGame.Application.Services;

namespace RedRiftGame;

internal class GameLobbyRunnerHostedService : IHostedService
{
    private readonly IGameLobbyRunner _gameLobbyRunner;
    private readonly IServiceScope _scope;
    private readonly CancellationTokenSource _cts;

    public GameLobbyRunnerHostedService(IServiceProvider serviceProvider)
    {
        _scope = serviceProvider.CreateScope();
        _gameLobbyRunner = _scope.ServiceProvider.GetRequiredService<IGameLobbyRunner>();
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
        _scope.Dispose();
        
        return Task.CompletedTask;
    }
}
