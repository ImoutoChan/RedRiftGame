using Microsoft.EntityFrameworkCore;
using RedRiftGame.Infrastructure.Persistence.Entities;

namespace RedRiftGame.Infrastructure.Persistence;

internal class RedRiftGameDbContext : DbContext
{
    public RedRiftGameDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<MatchEntity> Matches { get; set; } = default!;
}