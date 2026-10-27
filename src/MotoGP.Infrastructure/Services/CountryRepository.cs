using Microsoft.EntityFrameworkCore;
using MotoGP.Domain.Entities;
using MotoGP.Infrastructure.DbContexts;

namespace MotoGP.Infrastructure.Services;

public class CountryRepository : ICountryRepository
{
    private readonly MotoGPContext _context;

    public CountryRepository(MotoGPContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Country>> GetCountriesAsync(bool onlyWithTickets = false)
    {
        IQueryable<Country> query = _context.Countries;

        if (onlyWithTickets)
        {
            query = query.Where(c => c.Tickets.Any());
        }

        return await query.OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<bool> CountryExistsAsync(int countryId)
    {
        return await _context.Countries.AnyAsync(c => c.Id == countryId);
    }
}
