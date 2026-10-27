using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using MotoGP.Api.Controllers;
using MotoGP.Contracts.Models;
using MotoGP.Api.MappingProfiles;
using MotoGP.Domain.Entities;
using MotoGP.Infrastructure.DbContexts;
using MotoGP.Infrastructure.Services;

namespace MotoGP.Api.Tests.Controllers;

[TestClass]
public class TicketsControllerTests
{
    private static (ITicketRepository TicketRepository, IRaceRepository RaceRepository, ICountryRepository CountryRepository, MotoGPContext Context, IMapper Mapper) CreateRepositoriesAndMapper(string dbName)
    {
        var options = new DbContextOptionsBuilder<MotoGPContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        var context = new MotoGPContext(options);

        var country = new Country("Belgium") { Id = 15 };
        var race = new Race("Circuit de la Sarthe") { Id = 5, Place = "Le Mans (France)", Description = "d", Length = 4185, Date = new DateTime(2026, 5, 18), X = 564, Y = 161 };
        context.Countries.Add(country);
        context.Races.Add(race);
        context.Tickets.Add(new Ticket("Existing Customer")
        {
            Id = 1,
            Email = "existing@example.com",
            Address = "Somewhere 1",
            CountryId = 15,
            RaceId = 5,
            Number = 1,
            OrderDate = new DateTime(2026, 1, 1),
            Paid = false
        });
        context.SaveChanges();

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<TicketProfile>();
            cfg.AddProfile<CountryProfile>();
            cfg.AddProfile<RaceProfile>();
        }, NullLoggerFactory.Instance);

        return (new TicketRepository(context, NullLogger<TicketRepository>.Instance), new RaceRepository(context), new CountryRepository(context), context, configuration.CreateMapper());
    }

    [TestMethod]
    public async Task GetTickets_SetsXPaginationHeaderWithMetadata()
    {
        var (ticketRepo, raceRepo, countryRepo, _, mapper) = CreateRepositoriesAndMapper(nameof(GetTickets_SetsXPaginationHeaderWithMetadata));
        var controller = new TicketsController(ticketRepo, raceRepo, countryRepo, mapper, NullLogger<TicketsController>.Instance)
        {
            ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
        };

        var result = await controller.GetTickets();

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        var tickets = (okResult.Value as IEnumerable<TicketListDto>)!.ToList();
        Assert.HasCount(1, tickets);
        Assert.IsTrue(controller.Response.Headers.ContainsKey("X-Pagination"));
        StringAssert.Contains(controller.Response.Headers["X-Pagination"].ToString(), "\"TotalItemCount\":1");
    }

    [TestMethod]
    public async Task CreateTicket_WithUnknownCountryId_ReturnsBadRequestWithExpectedMessage()
    {
        var (ticketRepo, raceRepo, countryRepo, _, mapper) = CreateRepositoriesAndMapper(nameof(CreateTicket_WithUnknownCountryId_ReturnsBadRequestWithExpectedMessage));
        var controller = new TicketsController(ticketRepo, raceRepo, countryRepo, mapper, NullLogger<TicketsController>.Instance);

        var dto = new TicketForCreationDto
        {
            Name = "Jef Peeters",
            Email = "jp@ping.be",
            Address = "Nieuwstraat 15, 1000 Brussels",
            CountryId = 9999,
            RaceId = 5,
            Number = 2
        };

        var result = await controller.CreateTicket(dto);

        var badRequest = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequest);
        Assert.AreEqual("Race/Country with Id 9999 does not exist.", badRequest.Value);
    }

    [TestMethod]
    public async Task CreateTicket_WithUnknownRaceId_ReturnsBadRequestWithExpectedMessage()
    {
        var (ticketRepo, raceRepo, countryRepo, _, mapper) = CreateRepositoriesAndMapper(nameof(CreateTicket_WithUnknownRaceId_ReturnsBadRequestWithExpectedMessage));
        var controller = new TicketsController(ticketRepo, raceRepo, countryRepo, mapper, NullLogger<TicketsController>.Instance);

        var dto = new TicketForCreationDto
        {
            Name = "Jef Peeters",
            Email = "jp@ping.be",
            Address = "Nieuwstraat 15, 1000 Brussels",
            CountryId = 15,
            RaceId = 9999,
            Number = 2
        };

        var result = await controller.CreateTicket(dto);

        var badRequest = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequest);
        Assert.AreEqual("Race/Country with Id 9999 does not exist.", badRequest.Value);
    }

    [TestMethod]
    public async Task CreateTicket_WithValidData_SetsOrderDateAndPaidAndLoadsRaceName()
    {
        var (ticketRepo, raceRepo, countryRepo, context, mapper) = CreateRepositoriesAndMapper(nameof(CreateTicket_WithValidData_SetsOrderDateAndPaidAndLoadsRaceName));
        var controller = new TicketsController(ticketRepo, raceRepo, countryRepo, mapper, NullLogger<TicketsController>.Instance);

        var dto = new TicketForCreationDto
        {
            Name = "Jef Peeters",
            Email = "jp@ping.be",
            Address = "Nieuwstraat 15, 1000 Brussels",
            CountryId = 15,
            RaceId = 5,
            Number = 2
        };

        var result = await controller.CreateTicket(dto);

        var created = result.Result as CreatedAtRouteResult;
        Assert.IsNotNull(created);
        var ticketDto = created.Value as TicketDetailsDto;
        Assert.IsNotNull(ticketDto);
        Assert.AreEqual("Circuit de la Sarthe", ticketDto.RaceName);
        Assert.AreEqual("18/05", ticketDto.ShortRaceDate);
        Assert.IsFalse(ticketDto.Paid);
        Assert.AreEqual(DateTime.Now.Date, ticketDto.OrderDate.Date);

        var persisted = context.Tickets.Single(t => t.Id == ticketDto.Id);
        Assert.IsFalse(persisted.Paid);
    }

    [TestMethod]
    public async Task PatchTicket_SetsPaidToTrue()
    {
        var (ticketRepo, raceRepo, countryRepo, context, mapper) = CreateRepositoriesAndMapper(nameof(PatchTicket_SetsPaidToTrue));
        var controller = new TicketsController(ticketRepo, raceRepo, countryRepo, mapper, NullLogger<TicketsController>.Instance);

        var result = await controller.PatchTicket(1, new TicketForPatchDto { Paid = true });

        Assert.IsInstanceOfType<NoContentResult>(result);
        Assert.IsTrue(context.Tickets.Single(t => t.Id == 1).Paid);
    }

    [TestMethod]
    public async Task PatchTicket_UnknownId_ReturnsNotFound()
    {
        var (ticketRepo, raceRepo, countryRepo, _, mapper) = CreateRepositoriesAndMapper(nameof(PatchTicket_UnknownId_ReturnsNotFound));
        var controller = new TicketsController(ticketRepo, raceRepo, countryRepo, mapper, NullLogger<TicketsController>.Instance);

        var result = await controller.PatchTicket(9999, new TicketForPatchDto { Paid = true });

        Assert.IsInstanceOfType<NotFoundResult>(result);
    }
}
