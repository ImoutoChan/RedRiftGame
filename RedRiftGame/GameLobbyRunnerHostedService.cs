using RedRiftGame.Application.Services;
using RedRiftGame.Application.Services.Runner;

namespace RedRiftGame;

internal class GameLobbyRunnerHostedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly CancellationTokenSource _cts;

    public GameLobbyRunnerHostedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _cts = new CancellationTokenSource();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(async () =>
        {
            using var scope = _serviceProvider.CreateScope();
            var runner = scope.ServiceProvider.GetRequiredService<IGameLobbyRunner>();
            await runner.RunMatchesAsync(_cts.Token);
        }, cancellationToken);
        
        Task.Run(async () =>
        {
            using var scope = _serviceProvider.CreateScope();
            var runner = scope.ServiceProvider.GetRequiredService<IGameLobbyRunner>();
            await runner.ReportMatchesAsync(_cts.Token);
        }, cancellationToken);

        return Task.FromResult(Task.CompletedTask);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _cts.Cancel();

        return Task.CompletedTask;
    }
}
