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
public class RidersControllerTests
{
    private static (IRiderRepository Repository, IMapper Mapper) CreateRepositoryAndMapper(string dbName)
    {
        var options = new DbContextOptionsBuilder<MotoGPContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        var context = new MotoGPContext(options);

        var team = new Team("Repsol Honda Team") { Id = 5 };
        context.Teams.Add(team);
        context.Riders.Add(new Rider("Marquez", "Marc") { Id = 12, Number = 93, TeamId = 5, CountryId = 159 });
        context.Riders.Add(new Rider("Pedrosa", "Dani") { Id = 11, Number = 26, TeamId = 5, CountryId = 159 });
        context.SaveChanges();

        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<RiderProfile>(), NullLoggerFactory.Instance);
        return (new RiderRepository(context), configuration.CreateMapper());
    }

    [TestMethod]
    public async Task GetRiders_ReturnsRidersSortedByNumber()
    {
        var (repository, mapper) = CreateRepositoryAndMapper(nameof(GetRiders_ReturnsRidersSortedByNumber));
        var controller = new RidersController(repository, mapper, NullLogger<RidersController>.Instance);

        var result = await controller.GetRiders();

        var okResult = result.Result as OkObjectResult;
        var riders = okResult?.Value as IEnumerable<RiderListDto>;
        Assert.IsNotNull(riders);
        var riderList = riders.ToList();
        Assert.HasCount(2, riderList);
        Assert.AreEqual(26, riderList[0].Number);
        Assert.AreEqual(93, riderList[1].Number);
    }

    [TestMethod]
    public async Task GetRiders_IncludesTeamName()
    {
        var (repository, mapper) = CreateRepositoryAndMapper(nameof(GetRiders_IncludesTeamName));
        var controller = new RidersController(repository, mapper, NullLogger<RidersController>.Instance);

        var result = await controller.GetRiders();

        var okResult = result.Result as OkObjectResult;
        var riders = (okResult?.Value as IEnumerable<RiderListDto>)!.ToList();
        Assert.IsTrue(riders.All(r => r.TeamName == "Repsol Honda Team"));
    }
}
