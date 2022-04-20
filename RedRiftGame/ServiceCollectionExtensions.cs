using RedRiftGame.Application.Services;
using RedRiftGame.Services;

namespace RedRiftGame;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHost(this IServiceCollection services)
    {
        services
            .AddHostedService<MatchRunnerHostedService>()
            .AddHostedService<MatchReporterHostedService>()
            .AddTransient<IMatchClientReporter, SignalRMatchClientReporter>()
            .AddSignalR(x => x.EnableDetailedErrors = true);

        return services;
    }
}
