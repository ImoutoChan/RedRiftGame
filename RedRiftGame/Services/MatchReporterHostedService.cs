using RedRiftGame.Application.Services.Runner;

namespace RedRiftGame.Services;

internal class MatchReporterHostedService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public MatchReporterHostedService(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMatchReporter>();
        await runner.ReportAsync(stoppingToken);
    }
}
