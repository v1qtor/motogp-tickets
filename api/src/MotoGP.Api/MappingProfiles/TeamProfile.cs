using AutoMapper;
using MotoGP.Contracts.Models;
using MotoGP.Domain.Entities;

namespace MotoGP.Api.MappingProfiles;

public class TeamProfile : Profile
{
    public TeamProfile()
    {
        CreateMap<Team, TeamListDto>();

        CreateMap<Team, TeamWithSponsorsDto>()
            .ForMember(dest => dest.Sponsors, opt => opt.MapFrom(src => src.TeamSponsors.Select(ts => ts.Sponsor)));

        CreateMap<Sponsor, SponsorDto>();
    }
}
