using MotoGP.Domain.Entities;

namespace MotoGP.Infrastructure.Services;

public interface ITeamRepository
{
    /// <summary>Returns all teams, sorted on Name, including all riders in the team.</summary>
    Task<IEnumerable<Team>> GetTeamsAsync();

    /// <summary>Returns one team by id, optionally including its Riders or its Sponsors.</summary>
    Task<Team?> GetTeamAsync(int teamId, TeamInclude include = TeamInclude.None);
}
