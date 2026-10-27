using AutoMapper;
using MotoGP.Contracts.Models;
using MotoGP.Domain.Entities;

namespace MotoGP.Api.MappingProfiles;

public class RaceProfile : Profile
{
    public RaceProfile()
    {
        CreateMap<Race, RaceDetailsDto>()
            .ForMember(dest => dest.ShortDate, opt => opt.MapFrom(src => src.Date.ToString("dd/MM")))
            .ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.Date.Month))
            .ForMember(dest => dest.TrackLength, opt => opt.MapFrom(src => src.Length / 1000.0));

        CreateMap<Race, RaceListDto>()
            .ForMember(dest => dest.ShortDate, opt => opt.MapFrom(src => src.Date.ToString("dd/MM")))
            .ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.Date.Month));
    }
}
