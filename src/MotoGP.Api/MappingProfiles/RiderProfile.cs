using AutoMapper;
using MotoGP.Contracts.Models;
using MotoGP.Domain.Entities;

namespace MotoGP.Api.MappingProfiles;

public class RiderProfile : Profile
{
    public RiderProfile()
    {
        CreateMap<Rider, RiderListDto>()
            .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team.Name))
            .ForMember(dest => dest.FlagNumber, opt => opt.MapFrom(src => src.CountryId));
    }
}
