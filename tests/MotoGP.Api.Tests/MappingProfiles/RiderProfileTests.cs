using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using MotoGP.Contracts.Models;
using MotoGP.Api.MappingProfiles;
using MotoGP.Domain.Entities;

namespace MotoGP.Api.Tests.MappingProfiles;

[TestClass]
public class RiderProfileTests
{
    private static IMapper CreateMapper()
    {
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<RiderProfile>(), NullLoggerFactory.Instance);
        return configuration.CreateMapper();
    }

    [TestMethod]
    public void Map_RiderToRiderListDto_UsesTeamNameAndCountryIdAsFlagNumber()
    {
        var mapper = CreateMapper();
        var team = new Team("Repsol Honda Team") { Id = 5 };
        var rider = new Rider("Marquez", "Marc") { Id = 12, Number = 93, CountryId = 159, TeamId = 5, Team = team };

        var dto = mapper.Map<RiderListDto>(rider);

        Assert.AreEqual("Marquez", dto.LastName);
        Assert.AreEqual("Marc", dto.FirstName);
        Assert.AreEqual(93, dto.Number);
        Assert.AreEqual("Repsol Honda Team", dto.TeamName);
        Assert.AreEqual(159, dto.FlagNumber);
    }

    [TestMethod]
    [DataRow(8, 159)]
    [DataRow(63, 58)]
    [DataRow(17, 44)]
    public void Map_RiderToRiderListDto_PreservesNumberAndFlagNumber(int number, int countryId)
    {
        var mapper = CreateMapper();
        var rider = new Rider("Test", "Rider") { Number = number, CountryId = countryId, Team = new Team("Team") };

        var dto = mapper.Map<RiderListDto>(rider);

        Assert.AreEqual(number, dto.Number);
        Assert.AreEqual(countryId, dto.FlagNumber);
    }
}
