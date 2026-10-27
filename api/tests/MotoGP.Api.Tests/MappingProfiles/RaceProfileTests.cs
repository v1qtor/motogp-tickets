using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using MotoGP.Contracts.Models;
using MotoGP.Api.MappingProfiles;
using MotoGP.Domain.Entities;

namespace MotoGP.Api.Tests.MappingProfiles;

[TestClass]
public class RaceProfileTests
{
    private static IMapper CreateMapper()
    {
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<RaceProfile>(), NullLoggerFactory.Instance);
        return configuration.CreateMapper();
    }

    [TestMethod]
    public void Map_RaceToRaceDetailsDto_ComputesShortDateMonthAndTrackLength()
    {
        var mapper = CreateMapper();
        var race = new Race("TT Circuit Assen")
        {
            Id = 1,
            Place = "Assen (Netherlands)",
            Description = "Some description.",
            Length = 4542,
            Date = new DateTime(2026, 6, 28),
            X = 517,
            Y = 19
        };

        var dto = mapper.Map<RaceDetailsDto>(race);

        Assert.AreEqual("28/06", dto.ShortDate);
        Assert.AreEqual(6, dto.Month);
        Assert.AreEqual(4.542, dto.TrackLength, 0.0001);
        Assert.AreEqual("Some description.", dto.Description);
    }

    [TestMethod]
    public void Map_RaceToRaceListDto_ComputesShortDateAndMonth()
    {
        var mapper = CreateMapper();
        var race = new Race("Losail Circuit")
        {
            Id = 2,
            Place = "Doha (Qatar)",
            Description = "Some description.",
            Length = 5380,
            Date = new DateTime(2026, 4, 23),
            X = 859,
            Y = 249
        };

        var dto = mapper.Map<RaceListDto>(race);

        Assert.AreEqual("23/04", dto.ShortDate);
        Assert.AreEqual(4, dto.Month);
    }
}
