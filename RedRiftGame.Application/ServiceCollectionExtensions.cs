using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using RedRiftGame.Application.Services;

namespace RedRiftGame.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IGameLobby, GameLobby>();
        services.AddSingleton<IGameLobbyRunner, GameLobbyRunner>();
        services.AddTransient<IClock, SystemClock>();

        services.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);

        return services;
    }
}