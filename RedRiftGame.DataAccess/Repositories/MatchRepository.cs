using AutoMapper;
using RedRiftGame.Application.Services;
using RedRiftGame.DataAccess.Entities;
using RedRiftGame.Domain;

namespace RedRiftGame.DataAccess.Repositories;

internal class MatchRepository : IMatchRepository
{
    private readonly RedRiftGameDbContext _context;
    private readonly IMapper _mapper;

    public MatchRepository(RedRiftGameDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task AppendAsync(Match match)
    {
        var entity = _mapper.Map<MatchEntity>(match);
        _context.Matches.Add(entity);
        await _context.SaveChangesAsync();
    }
}
