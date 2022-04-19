using RedRiftGame.Application.Services;
using RedRiftGame.Domain;
using RedRiftGame.Infrastructure.Persistence.Entities;

namespace RedRiftGame.Infrastructure.Persistence.Repositories;

internal class MatchRepository : IMatchRepository
{
    private readonly RedRiftGameDbContext _context;

    public MatchRepository(RedRiftGameDbContext context) => _context = context;

    public async Task AppendAsync(Match match)
    {
        // todo map match to entity
        _context.Matches.Add(new MatchEntity
        {
            Id = match.Id,
            HostName = match.Host.Name,
            GuestName = match.Guest!.Name,
            HostFinalHealth = match.Host.Health,
            GuestFinalHealth = match.Guest!.Health,
            TotalTurnsPlayed = match.CurrentTurn,
            FinishedAt = match.FinishedAt!.Value
        });

        await _context.SaveChangesAsync();
    }
}