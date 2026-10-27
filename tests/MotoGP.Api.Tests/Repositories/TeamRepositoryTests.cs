using Microsoft.EntityFrameworkCore;
using MotoGP.Domain.Entities;
using MotoGP.Infrastructure.DbContexts;
using MotoGP.Infrastructure.Services;

namespace MotoGP.Api.Tests.Repositories;

[TestClass]
public class TeamRepositoryTests
{
    private static MotoGPContext CreateContext(string dbName)
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

        return context;
    }

    [TestMethod]
    public async Task GetTeamAsync_IncludeNone_DoesNotLoadRidersOrSponsors()
    {
        var context = CreateContext(nameof(GetTeamAsync_IncludeNone_DoesNotLoadRidersOrSponsors));
        var repository = new TeamRepository(context);

        var team = await repository.GetTeamAsync(3);

        Assert.IsNotNull(team);
        Assert.IsFalse(context.Entry(team).Collection(t => t.Riders).IsLoaded);
    }

    [TestMethod]
    public async Task GetTeamAsync_IncludeRiders_LoadsRiders()
    {
        var context = CreateContext(nameof(GetTeamAsync_IncludeRiders_LoadsRiders));
        var repository = new TeamRepository(context);

        var team = await repository.GetTeamAsync(3, TeamInclude.Riders);

        Assert.IsNotNull(team);
        Assert.HasCount(1, team.Riders);
    }

    [TestMethod]
    public async Task GetTeamAsync_IncludeSponsors_LoadsSponsors()
    {
        var context = CreateContext(nameof(GetTeamAsync_IncludeSponsors_LoadsSponsors));
        var repository = new TeamRepository(context);

        var team = await repository.GetTeamAsync(3, TeamInclude.Sponsors);

        Assert.IsNotNull(team);
        Assert.HasCount(1, team.TeamSponsors);
        Assert.AreEqual("Michelin", team.TeamSponsors.First().Sponsor.Name);
    }

    [TestMethod]
    public async Task GetTeamAsync_UnknownId_ReturnsNull()
    {
        var context = CreateContext(nameof(GetTeamAsync_UnknownId_ReturnsNull));
        var repository = new TeamRepository(context);

        var team = await repository.GetTeamAsync(9999);

        Assert.IsNull(team);
    }
}
