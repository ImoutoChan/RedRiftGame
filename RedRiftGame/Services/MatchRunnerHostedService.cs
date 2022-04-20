using RedRiftGame.Application.Services.Runner;

namespace RedRiftGame.Services;

internal class MatchRunnerHostedService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public MatchRunnerHostedService(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMatchRunner>();
        await runner.RunAsync(stoppingToken);
    }
}