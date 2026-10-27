using Microsoft.EntityFrameworkCore;
using MotoGP.Domain.Entities;
using MotoGP.Infrastructure.DbContexts;

namespace MotoGP.Infrastructure.Services;

public class TeamRepository : ITeamRepository
{
    private readonly MotoGPContext _context;

    public TeamRepository(MotoGPContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Team>> GetTeamsAsync()
    {
        return await _context.Teams
            .Include(t => t.Riders)
            .OrderBy(t => t.Name)
            .ToListAsync();
    }

    public async Task<Team?> GetTeamAsync(int teamId, TeamInclude include = TeamInclude.None)
    {
        IQueryable<Team> query = _context.Teams;

        query = include switch
        {
            TeamInclude.Riders => query.Include(t => t.Riders),
            TeamInclude.Sponsors => query.Include(t => t.TeamSponsors).ThenInclude(ts => ts.Sponsor),
            _ => query
        };

        return await query.FirstOrDefaultAsync(t => t.Id == teamId);
    }
}
