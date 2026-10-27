using MotoGP.Domain.Entities;

namespace MotoGP.Domain.Strategies;

/// <summary>Estimates race duration as total distance (LapCount x track Length) divided by AvgSpeed.</summary>
public class DistanceBasedCalculator : IRaceDurationCalculator
{
    public TimeSpan CalculateDuration(Race race)
    {
        if (race.LapCount <= 0)
        {
            throw new InvalidOperationException($"Cannot calculate race duration: LapCount must be greater than zero (was {race.LapCount}).");
        }

        if (race.Length <= 0)
        {
            throw new InvalidOperationException($"Cannot calculate race duration: track Length must be greater than zero (was {race.Length}).");
        }

        if (race.AvgSpeed <= 0)
        {
            throw new InvalidOperationException($"Cannot calculate race duration: AvgSpeed must be greater than zero (was {race.AvgSpeed}).");
        }

        var totalDistanceKm = race.LapCount * (race.Length / 1000.0);
        var hours = totalDistanceKm / race.AvgSpeed;

        return TimeSpan.FromHours(hours);
    }
}
