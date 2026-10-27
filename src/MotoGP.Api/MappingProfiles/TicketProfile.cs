using AutoMapper;
using MotoGP.Contracts.Models;
using MotoGP.Domain.Entities;

namespace MotoGP.Api.MappingProfiles;

public class TicketProfile : Profile
{
    public TicketProfile()
    {
        CreateMap<Ticket, TicketListDto>()
            .ForMember(dest => dest.ShortOrderDate, opt => opt.MapFrom(src => src.OrderDate.ToString("dd/MM")))
            .ForMember(dest => dest.RaceName, opt => opt.MapFrom(src => src.Race.Name));

        CreateMap<Ticket, TicketDetailsDto>()
            .ForMember(dest => dest.RaceName, opt => opt.MapFrom(src => src.Race.Name))
            .ForMember(dest => dest.ShortRaceDate, opt => opt.MapFrom(src => src.Race.Date.ToString("dd/MM")));

        CreateMap<TicketForCreationDto, Ticket>()
            .ConstructUsing(src => new Ticket(src.Name!));

        // Only Paid is patchable; other properties are intentionally left unmapped.
        CreateMap<TicketForPatchDto, Ticket>()
            .ForMember(dest => dest.Paid, opt => opt.MapFrom(src => src.Paid!.Value));
    }
}
