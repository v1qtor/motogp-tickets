using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using MotoGP.Domain.Entities;
using MotoGP.Infrastructure.DbContexts;
using MotoGP.Infrastructure.Services;

namespace MotoGP.Api.Tests.Repositories;

[TestClass]
public class TicketRepositoryTests
{
    private static MotoGPContext CreateContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<MotoGPContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        var context = new MotoGPContext(options);

        var race1 = new Race("TT Circuit Assen") { Id = 1 };
        var race2 = new Race("Losail Circuit") { Id = 2 };
        context.Races.AddRange(race1, race2);
        context.Countries.Add(new Country("Belgium") { Id = 15 });
        context.Countries.Add(new Country("France") { Id = 58 });

        for (var i = 1; i <= 15; i++)
        {
            context.Tickets.Add(new Ticket(i <= 10 ? $"Jef Peeters {i}" : $"Anna Smith {i}")
            {
                Id = i,
                Email = i <= 10 ? $"jef{i}@ping.be" : $"anna{i}@example.com",
                Address = "Somewhere",
                CountryId = i % 2 == 0 ? 15 : 58,
                RaceId = i % 2 == 0 ? 1 : 2,
                Number = 1,
                OrderDate = new DateTime(2026, 1, 1).AddDays(i),
                Paid = i % 3 == 0
            });
        }

        context.SaveChanges();
        return context;
    }

    [TestMethod]
    public async Task GetTicketsAsync_NoFilters_ReturnsFirstPageOfDefaultSize()
    {
        var repository = new TicketRepository(CreateContext(nameof(GetTicketsAsync_NoFilters_ReturnsFirstPageOfDefaultSize)), NullLogger<TicketRepository>.Instance);

        var (tickets, pagination) = await repository.GetTicketsAsync(null, null, null);

        Assert.HasCount(10, tickets.ToList());
        Assert.AreEqual(15, pagination.TotalItemCount);
        Assert.AreEqual(2, pagination.TotalPageCount);
        Assert.AreEqual(1, pagination.CurrentPage);
        Assert.AreEqual(10, pagination.PageSize);
    }

    [TestMethod]
    public async Task GetTicketsAsync_SecondPage_ReturnsRemainingItems()
    {
        var repository = new TicketRepository(CreateContext(nameof(GetTicketsAsync_SecondPage_ReturnsRemainingItems)), NullLogger<TicketRepository>.Instance);

        var (tickets, pagination) = await repository.GetTicketsAsync(null, null, null, pageNumber: 2, pageSize: 10);

        Assert.HasCount(5, tickets.ToList());
        Assert.AreEqual(2, pagination.CurrentPage);
    }

    [TestMethod]
    public async Task GetTicketsAsync_SearchQuery_MatchesNameOrEmail()
    {
        var repository = new TicketRepository(CreateContext(nameof(GetTicketsAsync_SearchQuery_MatchesNameOrEmail)), NullLogger<TicketRepository>.Instance);

        var (tickets, pagination) = await repository.GetTicketsAsync("Anna", null, null, pageSize: 50);

        Assert.AreEqual(5, pagination.TotalItemCount);
        Assert.IsTrue(tickets.All(t => t.Name.Contains("Anna")));
    }

    [TestMethod]
    public async Task GetTicketsAsync_CountryIdFilter_ReturnsOnlyMatchingCountry()
    {
        var repository = new TicketRepository(CreateContext(nameof(GetTicketsAsync_CountryIdFilter_ReturnsOnlyMatchingCountry)), NullLogger<TicketRepository>.Instance);

        var (tickets, _) = await repository.GetTicketsAsync(null, countryId: 15, null, pageSize: 50);

        Assert.IsTrue(tickets.All(t => t.CountryId == 15));
    }

    [TestMethod]
    public async Task GetTicketsAsync_OpenTrue_ReturnsOnlyUnpaidTickets()
    {
        var repository = new TicketRepository(CreateContext(nameof(GetTicketsAsync_OpenTrue_ReturnsOnlyUnpaidTickets)), NullLogger<TicketRepository>.Instance);

        var (tickets, _) = await repository.GetTicketsAsync(null, null, null, open: true, pageSize: 50);

        Assert.IsTrue(tickets.All(t => !t.Paid));
    }

    [TestMethod]
    public async Task GetTicketsAsync_PageSizeAboveMax_IsCappedAtMax()
    {
        var repository = new TicketRepository(CreateContext(nameof(GetTicketsAsync_PageSizeAboveMax_IsCappedAtMax)), NullLogger<TicketRepository>.Instance);

        var (_, pagination) = await repository.GetTicketsAsync(null, null, null, pageSize: 5000);

        Assert.AreEqual(50, pagination.PageSize);
    }
}
