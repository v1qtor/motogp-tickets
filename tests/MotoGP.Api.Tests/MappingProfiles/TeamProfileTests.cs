using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using MotoGP.Contracts.Models;
using MotoGP.Api.MappingProfiles;
using MotoGP.Domain.Entities;

namespace MotoGP.Api.Tests.MappingProfiles;

[TestClass]
public class TeamProfileTests
{
    private static IMapper CreateMapper()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<TeamProfile>();
            cfg.AddProfile<RiderProfile>();
        }, NullLoggerFactory.Instance);
        return configuration.CreateMapper();
    }

    [TestMethod]
    public void Map_TeamToTeamListDto_IncludesRiders()
    {
        var mapper = CreateMapper();
        var team = new Team("Repsol Honda Team") { Id = 5, Logo = "Repsol.png" };
        team.Riders.Add(new Rider("Pedrosa", "Dani") { Id = 11, Number = 26, TeamId = 5, CountryId = 159 });

        var dto = mapper.Map<TeamListDto>(team);

        Assert.AreEqual("Repsol Honda Team", dto.Name);
        Assert.HasCount(1, dto.Riders);
        Assert.AreEqual("Pedrosa", dto.Riders[0].LastName);
    }

    [TestMethod]
    public void Map_TeamToTeamWithSponsorsDto_FlattensTeamSponsorsToSponsors()
    {
        var mapper = CreateMapper();
        var team = new Team("Ducati Team") { Id = 3 };
        var sponsor = new Sponsor("Michelin") { Id = 5, Logo = "michelin.png", OfficialWebsite = "https://www.michelinmotorsport.com/" };
        team.TeamSponsors.Add(new TeamSponsor { Id = 14, TeamId = 3, SponsorId = 5, Sponsor = sponsor });

        var dto = mapper.Map<TeamWithSponsorsDto>(team);

        Assert.HasCount(1, dto.Sponsors);
        Assert.AreEqual("Michelin", dto.Sponsors[0].Name);
        Assert.AreEqual("https://www.michelinmotorsport.com/", dto.Sponsors[0].OfficialWebsite);
    }

    [TestMethod]
    public void Configuration_IsValid()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<TeamProfile>();
            cfg.AddProfile<RiderProfile>();
        }, NullLoggerFactory.Instance);

        configuration.AssertConfigurationIsValid();
    }
}
