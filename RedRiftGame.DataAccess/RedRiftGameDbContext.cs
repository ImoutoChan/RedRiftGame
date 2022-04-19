using Microsoft.EntityFrameworkCore;
using RedRiftGame.DataAccess.Entities;

namespace RedRiftGame.DataAccess;

internal class RedRiftGameDbContext : DbContext
{
    public RedRiftGameDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<MatchEntity> Matches { get; set; } = default!;
}