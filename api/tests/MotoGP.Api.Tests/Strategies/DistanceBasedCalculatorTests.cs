using MotoGP.Domain.Strategies;
using MotoGP.Domain.Entities;

namespace MotoGP.Api.Tests.Strategies;

[TestClass]
public class DistanceBasedCalculatorTests
{
    [TestMethod]
    public void CalculateDuration_ValidRace_DividesTotalDistanceByAvgSpeed()
    {
        var calculator = new DistanceBasedCalculator();
        // 26 laps * 4.542 km = 118.092 km, at 160 km/h.
        var race = new Race("TT Circuit Assen") { LapCount = 26, Length = 4542, AvgSpeed = 160 };

        var duration = calculator.CalculateDuration(race);

        var expectedHours = (26 * 4.542) / 160;
        Assert.AreEqual(TimeSpan.FromHours(expectedHours), duration);
    }

    [TestMethod]
    public void CalculateDuration_ZeroAvgSpeed_ThrowsInvalidOperationException()
    {
        var calculator = new DistanceBasedCalculator();
        var race = new Race("TT Circuit Assen") { LapCount = 26, Length = 4542, AvgSpeed = 0 };

        Assert.ThrowsExactly<InvalidOperationException>(() => calculator.CalculateDuration(race));
    }

    [TestMethod]
    public void CalculateDuration_NegativeLength_ThrowsInvalidOperationException()
    {
        var calculator = new DistanceBasedCalculator();
        var race = new Race("TT Circuit Assen") { LapCount = 26, Length = -1, AvgSpeed = 160 };

        Assert.ThrowsExactly<InvalidOperationException>(() => calculator.CalculateDuration(race));
    }

    [TestMethod]
    public void CalculateDuration_ZeroLapCount_ThrowsInvalidOperationException()
    {
        var calculator = new DistanceBasedCalculator();
        var race = new Race("TT Circuit Assen") { LapCount = 0, Length = 4542, AvgSpeed = 160 };

        Assert.ThrowsExactly<InvalidOperationException>(() => calculator.CalculateDuration(race));
    }
}
