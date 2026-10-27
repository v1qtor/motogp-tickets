using System.ComponentModel.DataAnnotations;
using MotoGP.Contracts.Models;

namespace MotoGP.Api.Tests.Dtos;

[TestClass]
public class TicketForCreationDtoTests
{
    [TestMethod]
    public void Validate_EmptyDto_ReturnsRequiredErrorForEveryProperty()
    {
        var dto = new TicketForCreationDto();
        var context = new ValidationContext(dto);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(dto, context, results, validateAllProperties: true);

        Assert.IsFalse(isValid);
        Assert.HasCount(6, results);
        CollectionAssert.Contains(results.Select(r => r.ErrorMessage).ToList(), "Name is required.");
        CollectionAssert.Contains(results.Select(r => r.ErrorMessage).ToList(), "CountryId is required.");
        CollectionAssert.Contains(results.Select(r => r.ErrorMessage).ToList(), "RaceId is required.");
    }

    [TestMethod]
    public void Validate_FullyPopulatedDto_IsValid()
    {
        var dto = new TicketForCreationDto
        {
            Name = "Jef Peeters",
            Email = "jp@ping.be",
            Address = "Nieuwstraat 15, 1000 Brussels",
            CountryId = 15,
            RaceId = 5,
            Number = 2
        };
        var context = new ValidationContext(dto);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(dto, context, results, validateAllProperties: true);

        Assert.IsTrue(isValid);
        Assert.IsEmpty(results);
    }
}
