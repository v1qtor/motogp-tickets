using MotoGP.Domain.Entities;

namespace MotoGP.Domain.Strategies;

/// <summary>Estimates race duration as LapCount x LapTime.</summary>
public class LapsBasedCalculator : IRaceDurationCalculator
{
    public TimeSpan CalculateDuration(Race race)
    {
        if (race.LapCount <= 0)
        {
            throw new InvalidOperationException($"Cannot calculate race duration: LapCount must be greater than zero (was {race.LapCount}).");
        }

        if (race.LapTime <= 0)
        {
            throw new InvalidOperationException($"Cannot calculate race duration: LapTime must be greater than zero (was {race.LapTime}).");
        }

        return TimeSpan.FromSeconds(race.LapCount * race.LapTime);
    }
}
