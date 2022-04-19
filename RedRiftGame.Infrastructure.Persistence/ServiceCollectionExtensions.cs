using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedRiftGame.Application.Services;
using RedRiftGame.Infrastructure.Persistence.Repositories;

namespace RedRiftGame.Infrastructure.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<RedRiftGameDbContext>(builder
            => builder.UseNpgsql(configuration.GetConnectionString("RedRiftGameDatabase"), x => x.UseNodaTime()));

        services.AddTransient<IMatchRepository, MatchRepository>();

        return services;
    }
}