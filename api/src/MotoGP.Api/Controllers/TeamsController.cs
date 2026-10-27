using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MotoGP.Contracts.Models;
using MotoGP.Infrastructure.Services;

namespace MotoGP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    private readonly ITeamRepository _teamRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<TeamsController> _logger;

    public TeamsController(ITeamRepository teamRepository, IMapper mapper, ILogger<TeamsController> logger)
    {
        _teamRepository = teamRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Returns all teams, sorted on Name, including all riders in the team.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TeamListDto>>> GetTeams()
    {
        var teams = await _teamRepository.GetTeamsAsync();

        return Ok(_mapper.Map<IEnumerable<TeamListDto>>(teams));
    }

    /// <summary>
    /// Returns all riders belonging to the team with the given id.
    /// </summary>
    [HttpGet("{teamId}/riders")]
    public async Task<ActionResult<IEnumerable<RiderListDto>>> GetRidersForTeam(int teamId)
    {
        var team = await _teamRepository.GetTeamAsync(teamId, TeamInclude.Riders);

        if (team is null)
        {
            _logger.LogWarning("Team with Id {TeamId} wasn't found.", teamId);
            return NotFound();
        }

        return Ok(_mapper.Map<IEnumerable<RiderListDto>>(team.Riders));
    }

    /// <summary>
    /// Returns the details of one team plus its sponsors.
    /// </summary>
    [HttpGet("{teamId}/sponsors")]
    public async Task<ActionResult<TeamWithSponsorsDto>> GetSponsorsForTeam(int teamId)
    {
        var team = await _teamRepository.GetTeamAsync(teamId, TeamInclude.Sponsors);

        if (team is null)
        {
            _logger.LogWarning("Team with Id {TeamId} wasn't found.", teamId);
            return NotFound();
        }

        return Ok(_mapper.Map<TeamWithSponsorsDto>(team));
    }
}
