using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedRiftGame.Application.Services;
using RedRiftGame.DataAccess.Repositories;

namespace RedRiftGame.DataAccess;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<RedRiftGameDbContext>(builder
            => builder.UseNpgsql(configuration.GetConnectionString("RedRiftGameDatabase"), x => x.UseNodaTime()));

        services.AddTransient<IMatchRepository, MatchRepository>();
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);

        return services;
    }
}
