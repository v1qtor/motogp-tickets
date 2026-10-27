using MotoGP.Domain.Strategies;
using MotoGP.Domain.Entities;

namespace MotoGP.Api.Tests.Strategies;

[TestClass]
public class LapsBasedCalculatorTests
{
    [TestMethod]
    public void CalculateDuration_ValidRace_MultipliesLapCountByLapTime()
    {
        var calculator = new LapsBasedCalculator();
        var race = new Race("TT Circuit Assen") { LapCount = 26, LapTime = 105 };

        var duration = calculator.CalculateDuration(race);

        Assert.AreEqual(TimeSpan.FromSeconds(26 * 105), duration);
    }

    [TestMethod]
    public void CalculateDuration_ZeroLapCount_ThrowsInvalidOperationException()
    {
        var calculator = new LapsBasedCalculator();
        var race = new Race("TT Circuit Assen") { LapCount = 0, LapTime = 105 };

        Assert.ThrowsExactly<InvalidOperationException>(() => calculator.CalculateDuration(race));
    }

    [TestMethod]
    public void CalculateDuration_NegativeLapTime_ThrowsInvalidOperationException()
    {
        var calculator = new LapsBasedCalculator();
        var race = new Race("TT Circuit Assen") { LapCount = 26, LapTime = -1 };

        Assert.ThrowsExactly<InvalidOperationException>(() => calculator.CalculateDuration(race));
    }
}
