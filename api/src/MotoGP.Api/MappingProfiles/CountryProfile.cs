using AutoMapper;
using MotoGP.Contracts.Models;
using MotoGP.Domain.Entities;

namespace MotoGP.Api.MappingProfiles;

public class CountryProfile : Profile
{
    public CountryProfile()
    {
        CreateMap<Country, CountryListDto>();
    }
}
