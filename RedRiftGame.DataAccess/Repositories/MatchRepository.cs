using RedRiftGame.Application.Services;
using RedRiftGame.DataAccess.Entities;
using RedRiftGame.Domain;

namespace RedRiftGame.DataAccess.Repositories;

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
            GuestName = match.GetGuest().Name,
            HostFinalHealth = match.Host.Health,
            GuestFinalHealth = match.GetGuest().Health,
            TotalTurnsPlayed = match.CurrentTurn,
            FinishedAt = match.FinishedAt!.Value
        });

        await _context.SaveChangesAsync();
    }
}
