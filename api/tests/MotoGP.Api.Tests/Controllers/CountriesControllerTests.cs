using AutoMapper;
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
public class CountriesControllerTests
{
    private static (ICountryRepository Repository, IMapper Mapper) CreateRepositoryAndMapper(string dbName)
    {
        var options = new DbContextOptionsBuilder<MotoGPContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        var context = new MotoGPContext(options);
        var race = new Race("TT Circuit Assen") { Id = 1 };
        context.Races.Add(race);
        context.Countries.Add(new Country("Zambia") { Id = 192 });
        var belgium = new Country("Belgium") { Id = 15 };
        context.Countries.Add(belgium);
        context.Tickets.Add(new Ticket("Jasper De Smet") { Email = "j@example.com", Address = "a", CountryId = 15, RaceId = 1, Number = 1 });
        context.SaveChanges();

        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<CountryProfile>(), NullLoggerFactory.Instance);
        return (new CountryRepository(context), configuration.CreateMapper());
    }

    [TestMethod]
    public async Task GetCountries_ReturnsCountriesSortedByName()
    {
        var (repository, mapper) = CreateRepositoryAndMapper(nameof(GetCountries_ReturnsCountriesSortedByName));
        var controller = new CountriesController(repository, mapper, NullLogger<CountriesController>.Instance);

        var result = await controller.GetCountries();

        var okResult = result.Result as OkObjectResult;
        var countries = (okResult?.Value as IEnumerable<CountryListDto>)!.ToList();
        Assert.HasCount(2, countries);
        Assert.AreEqual("Belgium", countries[0].Name);
        Assert.AreEqual("Zambia", countries[1].Name);
    }

    [TestMethod]
    public async Task GetCountries_OnlyWithTickets_ReturnsOnlyCountriesWithSoldTickets()
    {
        var (repository, mapper) = CreateRepositoryAndMapper(nameof(GetCountries_OnlyWithTickets_ReturnsOnlyCountriesWithSoldTickets));
        var controller = new CountriesController(repository, mapper, NullLogger<CountriesController>.Instance);

        var result = await controller.GetCountries(onlyWithTickets: true);

        var okResult = result.Result as OkObjectResult;
        var countries = (okResult?.Value as IEnumerable<CountryListDto>)!.ToList();
        Assert.HasCount(1, countries);
        Assert.AreEqual("Belgium", countries[0].Name);
    }
}
