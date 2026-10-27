using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using MotoGP.Contracts.Models;
using MotoGP.Api.MappingProfiles;
using MotoGP.Domain.Entities;

namespace MotoGP.Api.Tests.MappingProfiles;

[TestClass]
public class TicketProfileTests
{
    private static IMapper CreateMapper()
    {
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<TicketProfile>(), NullLoggerFactory.Instance);
        return configuration.CreateMapper();
    }

    [TestMethod]
    public void Map_TicketToTicketListDto_ComputesShortOrderDateAndRaceName()
    {
        var mapper = CreateMapper();
        var race = new Race("TT Circuit Assen") { Id = 1 };
        var ticket = new Ticket("Liam Thompson")
        {
            Id = 1,
            Number = 1,
            Paid = false,
            OrderDate = new DateTime(2026, 3, 11),
            Race = race
        };

        var dto = mapper.Map<TicketListDto>(ticket);

        Assert.AreEqual("11/03", dto.ShortOrderDate);
        Assert.AreEqual("TT Circuit Assen", dto.RaceName);
        Assert.IsFalse(dto.Paid);
    }

    [TestMethod]
    public void Map_TicketToTicketDetailsDto_ComputesRaceNameAndShortRaceDate()
    {
        var mapper = CreateMapper();
        var race = new Race("Losail Circuit") { Id = 2, Date = new DateTime(2026, 4, 23) };
        var ticket = new Ticket("Jasper De Smet")
        {
            Id = 2,
            Email = "jasper.desmet@example.com",
            Address = "601 Bruges Street, Bruges",
            Race = race
        };

        var dto = mapper.Map<TicketDetailsDto>(ticket);

        Assert.AreEqual("Losail Circuit", dto.RaceName);
        Assert.AreEqual("23/04", dto.ShortRaceDate);
    }

    [TestMethod]
    public void Map_TicketForCreationDtoToTicket_ConstructsTicketWithName()
    {
        var mapper = CreateMapper();
        var dto = new TicketForCreationDto
        {
            Name = "Jef Peeters",
            Email = "jp@ping.be",
            Address = "Nieuwstraat 15, 1000 Brussels",
            CountryId = 15,
            RaceId = 5,
            Number = 2
        };

        var ticket = mapper.Map<Ticket>(dto);

        Assert.AreEqual("Jef Peeters", ticket.Name);
        Assert.AreEqual(15, ticket.CountryId);
        Assert.AreEqual(5, ticket.RaceId);
        Assert.AreEqual(2, ticket.Number);
    }

    [TestMethod]
    public void Map_TicketForPatchDtoToTicket_OnlyUpdatesPaid()
    {
        var mapper = CreateMapper();
        var ticket = new Ticket("Existing Customer") { Name = "Existing Customer", Paid = false, Number = 3 };
        var patchDto = new TicketForPatchDto { Paid = true };

        mapper.Map(patchDto, ticket);

        Assert.IsTrue(ticket.Paid);
        Assert.AreEqual("Existing Customer", ticket.Name);
        Assert.AreEqual(3, ticket.Number);
    }
}
