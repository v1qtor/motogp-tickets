using Microsoft.EntityFrameworkCore;
using MotoGP.Domain.Entities;
using MotoGP.Infrastructure.DbContexts;
using MotoGP.Infrastructure.Services;

namespace MotoGP.Api.Tests.Repositories;

[TestClass]
public class CountryRepositoryTests
{
    private static MotoGPContext CreateContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<MotoGPContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        var context = new MotoGPContext(options);
        var race = new Race("TT Circuit Assen") { Id = 1 };
        context.Races.Add(race);
        context.Countries.Add(new Country("Belgium") { Id = 15 });
        context.Countries.Add(new Country("France") { Id = 58 });
        context.Tickets.Add(new Ticket("Someone") { Email = "e@x.com", Address = "a", CountryId = 15, RaceId = 1, Number = 1 });
        context.SaveChanges();

        return context;
    }

    [TestMethod]
    public async Task CountryExistsAsync_KnownId_ReturnsTrue()
    {
        var repository = new CountryRepository(CreateContext(nameof(CountryExistsAsync_KnownId_ReturnsTrue)));

        var exists = await repository.CountryExistsAsync(15);

        Assert.IsTrue(exists);
    }

    [TestMethod]
    public async Task CountryExistsAsync_UnknownId_ReturnsFalse()
    {
        var repository = new CountryRepository(CreateContext(nameof(CountryExistsAsync_UnknownId_ReturnsFalse)));

        var exists = await repository.CountryExistsAsync(9999);

        Assert.IsFalse(exists);
    }

    [TestMethod]
    public async Task GetCountriesAsync_OnlyWithTicketsFalse_ReturnsAllCountries()
    {
        var repository = new CountryRepository(CreateContext(nameof(GetCountriesAsync_OnlyWithTicketsFalse_ReturnsAllCountries)));

        var countries = (await repository.GetCountriesAsync()).ToList();

        Assert.HasCount(2, countries);
    }

    [TestMethod]
    public async Task GetCountriesAsync_OnlyWithTicketsTrue_ReturnsOnlyCountriesWithTickets()
    {
        var repository = new CountryRepository(CreateContext(nameof(GetCountriesAsync_OnlyWithTicketsTrue_ReturnsOnlyCountriesWithTickets)));

        var countries = (await repository.GetCountriesAsync(onlyWithTickets: true)).ToList();

        Assert.HasCount(1, countries);
        Assert.AreEqual("Belgium", countries[0].Name);
    }
}
