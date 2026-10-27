using MotoGP.Domain.Entities;

namespace MotoGP.Infrastructure.Services;

public interface IRaceRepository
{
    /// <summary>Returns all races, sorted on Date.</summary>
    Task<IEnumerable<Race>> GetRacesAsync();

    Task<Race?> GetRaceAsync(int raceId);

    Task<bool> RaceExistsAsync(int raceId);
}
