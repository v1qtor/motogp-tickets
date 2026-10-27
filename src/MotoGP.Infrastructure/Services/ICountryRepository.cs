using MotoGP.Domain.Entities;

namespace MotoGP.Infrastructure.Services;

public interface ICountryRepository
{
    /// <summary>Returns all countries, sorted by Name. When onlyWithTickets is true, only countries with at least one sold ticket are returned.</summary>
    Task<IEnumerable<Country>> GetCountriesAsync(bool onlyWithTickets = false);

    Task<bool> CountryExistsAsync(int countryId);
}
