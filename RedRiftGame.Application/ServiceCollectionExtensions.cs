using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using RedRiftGame.Application.Services;
using RedRiftGame.Application.Services.Runner;

namespace RedRiftGame.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IGameLobby, GameLobby>();
        services.AddSingleton<IFinishedMatchesPipe, FinishedMatchesPipe>();
        
        services.AddTransient<IGameLobbyRunner, GameLobbyRunner>();
        services.AddTransient<IClock>(_ => SystemClock.Instance);

        services.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);

        return services;
    }
}
