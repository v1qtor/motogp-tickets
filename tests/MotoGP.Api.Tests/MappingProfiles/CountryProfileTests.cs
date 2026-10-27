using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using MotoGP.Contracts.Models;
using MotoGP.Api.MappingProfiles;
using MotoGP.Domain.Entities;

namespace MotoGP.Api.Tests.MappingProfiles;

[TestClass]
public class CountryProfileTests
{
    private static IMapper CreateMapper()
    {
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<CountryProfile>(), NullLoggerFactory.Instance);
        return configuration.CreateMapper();
    }

    [TestMethod]
    public void Map_CountryToCountryListDto_CopiesIdAndName()
    {
        var mapper = CreateMapper();
        var country = new Country("Belgium") { Id = 15 };

        var dto = mapper.Map<CountryListDto>(country);

        Assert.AreEqual(15, dto.Id);
        Assert.AreEqual("Belgium", dto.Name);
    }

    [TestMethod]
    public void Configuration_IsValid()
    {
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<CountryProfile>(), NullLoggerFactory.Instance);

        configuration.AssertConfigurationIsValid();
    }
}
