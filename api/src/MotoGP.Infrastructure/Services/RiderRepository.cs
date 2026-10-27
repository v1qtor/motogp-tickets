using Microsoft.EntityFrameworkCore;
using MotoGP.Domain.Entities;
using MotoGP.Infrastructure.DbContexts;

namespace MotoGP.Infrastructure.Services;

public class RiderRepository : IRiderRepository
{
    private readonly MotoGPContext _context;

    public RiderRepository(MotoGPContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Rider>> GetRidersAsync()
    {
        return await _context.Riders
            .Include(r => r.Team)
            .OrderBy(r => r.Number)
            .ToListAsync();
    }
}
