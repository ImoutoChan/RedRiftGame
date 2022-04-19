using AutoMapper;
using RedRiftGame.Domain;

namespace RedRiftGame.DataAccess.Entities;

internal class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Match, MatchEntity>()
            .ConstructUsing(match => new MatchEntity
            {
                Id = match.Id,
                HostName = match.Host.Name,
                GuestName = match.GetGuest().Name,
                HostFinalHealth = match.Host.Health,
                GuestFinalHealth = match.GetGuest().Health,
                TotalTurnsPlayed = match.CurrentTurn,
                FinishedAt = match.FinishedAt!.Value
            });
    }
}
