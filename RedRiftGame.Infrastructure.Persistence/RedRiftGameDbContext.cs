using Microsoft.EntityFrameworkCore;
using RedRiftGame.Infrastructure.Persistence.Entities;

namespace RedRiftGame.Infrastructure.Persistence;

internal class RedRiftGameDbContext : DbContext
{
    public DbSet<MatchEntity> Matches { get; set; } = default!;

    public RedRiftGameDbContext(DbContextOptions options)
        : base(options)
    {
    }
}
