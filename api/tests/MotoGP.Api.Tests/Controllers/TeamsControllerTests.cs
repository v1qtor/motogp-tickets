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
public class TeamsControllerTests
{
    private static (ITeamRepository Repository, IMapper Mapper) CreateRepositoryAndMapper(string dbName)
    {
        var options = new DbContextOptionsBuilder<MotoGPContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        var context = new MotoGPContext(options);

        var team = new Team("Ducati Team") { Id = 3 };
        context.Teams.Add(team);
        context.Riders.Add(new Rider("Dovizioso", "Andrea") { Id = 6, Number = 4, TeamId = 3, CountryId = 83 });
        var sponsor = new Sponsor("Michelin") { Id = 5 };
        context.Sponsors.Add(sponsor);
        context.TeamSponsors.Add(new TeamSponsor { Id = 14, TeamId = 3, SponsorId = 5 });
        context.SaveChanges();

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<TeamProfile>();
            cfg.AddProfile<RiderProfile>();
        }, NullLoggerFactory.Instance);
        return (new TeamRepository(context), configuration.CreateMapper());
    }

    [TestMethod]
    public async Task GetTeams_ReturnsTeamsWithRiders()
    {
        var (repository, mapper) = CreateRepositoryAndMapper(nameof(GetTeams_ReturnsTeamsWithRiders));
        var controller = new TeamsController(repository, mapper, NullLogger<TeamsController>.Instance);

        var result = await controller.GetTeams();

        var okResult = result.Result as OkObjectResult;
        var teams = (okResult?.Value as IEnumerable<TeamListDto>)!.ToList();
        Assert.HasCount(1, teams);
        Assert.HasCount(1, teams[0].Riders);
    }

    [TestMethod]
    public async Task GetRidersForTeam_UnknownTeamId_ReturnsNotFound()
    {
        var (repository, mapper) = CreateRepositoryAndMapper(nameof(GetRidersForTeam_UnknownTeamId_ReturnsNotFound));
        var controller = new TeamsController(repository, mapper, NullLogger<TeamsController>.Instance);

        var result = await controller.GetRidersForTeam(9999);

        Assert.IsInstanceOfType<NotFoundResult>(result.Result);
    }

    [TestMethod]
    public async Task GetRidersForTeam_KnownTeamId_ReturnsRiders()
    {
        var (repository, mapper) = CreateRepositoryAndMapper(nameof(GetRidersForTeam_KnownTeamId_ReturnsRiders));
        var controller = new TeamsController(repository, mapper, NullLogger<TeamsController>.Instance);

        var result = await controller.GetRidersForTeam(3);

        var okResult = result.Result as OkObjectResult;
        var riders = (okResult?.Value as IEnumerable<RiderListDto>)!.ToList();
        Assert.HasCount(1, riders);
        Assert.AreEqual("Dovizioso", riders[0].LastName);
    }

    [TestMethod]
    public async Task GetSponsorsForTeam_KnownTeamId_ReturnsSponsors()
    {
        var (repository, mapper) = CreateRepositoryAndMapper(nameof(GetSponsorsForTeam_KnownTeamId_ReturnsSponsors));
        var controller = new TeamsController(repository, mapper, NullLogger<TeamsController>.Instance);

        var result = await controller.GetSponsorsForTeam(3);

        var okResult = result.Result as OkObjectResult;
        var dto = okResult?.Value as TeamWithSponsorsDto;
        Assert.IsNotNull(dto);
        Assert.HasCount(1, dto.Sponsors);
        Assert.AreEqual("Michelin", dto.Sponsors[0].Name);
    }

    [TestMethod]
    public async Task GetSponsorsForTeam_UnknownTeamId_ReturnsNotFound()
    {
        var (repository, mapper) = CreateRepositoryAndMapper(nameof(GetSponsorsForTeam_UnknownTeamId_ReturnsNotFound));
        var controller = new TeamsController(repository, mapper, NullLogger<TeamsController>.Instance);

        var result = await controller.GetSponsorsForTeam(9999);

        Assert.IsInstanceOfType<NotFoundResult>(result.Result);
    }
}
