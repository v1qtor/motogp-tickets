using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using MotoGP.Api.Controllers;
using MotoGP.Domain.Strategies;
using MotoGP.Contracts.Models;
using MotoGP.Api.MappingProfiles;
using MotoGP.Domain.Entities;
using MotoGP.Infrastructure.DbContexts;
using MotoGP.Infrastructure.Services;

namespace MotoGP.Api.Tests.Controllers;

[TestClass]
public class RacesControllerTests
{
    private static (IRaceRepository Repository, IMapper Mapper) CreateRepositoryAndMapper(string dbName)
    {
        var options = new DbContextOptionsBuilder<MotoGPContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        var context = new MotoGPContext(options);
        context.Races.Add(new Race("Losail Circuit") { Id = 2, Place = "Doha (Qatar)", Description = "d", Length = 5380, Date = new DateTime(2026, 4, 23), LapCount = 22, LapTime = 110, AvgSpeed = 170 });
        context.Races.Add(new Race("TT Circuit Assen") { Id = 1, Place = "Assen (Netherlands)", Description = "d", Length = 4542, Date = new DateTime(2026, 6, 28), LapCount = 26, LapTime = 105, AvgSpeed = 160 });
        context.SaveChanges();

        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<RaceProfile>(), NullLoggerFactory.Instance);
        return (new RaceRepository(context), configuration.CreateMapper());
    }

    [TestMethod]
    public async Task GetRaces_ReturnsRacesSortedByDate()
    {
        var (repository, mapper) = CreateRepositoryAndMapper(nameof(GetRaces_ReturnsRacesSortedByDate));
        var controller = new RacesController(repository, mapper, Mock.Of<IRaceDurationCalculator>(), NullLogger<RacesController>.Instance);

        var result = await controller.GetRaces();

        var okResult = result.Result as OkObjectResult;
        var races = (okResult?.Value as IEnumerable<RaceListDto>)!.ToList();
        Assert.HasCount(2, races);
        Assert.AreEqual("Losail Circuit", races[0].Name);
        Assert.AreEqual("TT Circuit Assen", races[1].Name);
    }

    [TestMethod]
    public async Task GetRace_KnownId_ReturnsRaceDetailsWithDurationFromInjectedCalculator()
    {
        var (repository, mapper) = CreateRepositoryAndMapper(nameof(GetRace_KnownId_ReturnsRaceDetailsWithDurationFromInjectedCalculator));
        var expectedDuration = TimeSpan.FromMinutes(42);
        var calculatorMock = new Mock<IRaceDurationCalculator>();
        calculatorMock.Setup(c => c.CalculateDuration(It.IsAny<Race>())).Returns(expectedDuration);
        var controller = new RacesController(repository, mapper, calculatorMock.Object, NullLogger<RacesController>.Instance);

        var result = await controller.GetRace(1);

        var okResult = result.Result as OkObjectResult;
        var dto = okResult?.Value as RaceDetailsDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual("TT Circuit Assen", dto.Name);
        Assert.AreEqual("28/06", dto.ShortDate);
        Assert.AreEqual(expectedDuration, dto.Duration);
        calculatorMock.Verify(c => c.CalculateDuration(It.Is<Race>(r => r.Id == 1)), Times.Once);
    }

    [TestMethod]
    public async Task GetRace_UnknownId_ReturnsNotFound()
    {
        var (repository, mapper) = CreateRepositoryAndMapper(nameof(GetRace_UnknownId_ReturnsNotFound));
        var controller = new RacesController(repository, mapper, Mock.Of<IRaceDurationCalculator>(), NullLogger<RacesController>.Instance);

        var result = await controller.GetRace(9999);

        Assert.IsInstanceOfType<NotFoundResult>(result.Result);
    }
}
