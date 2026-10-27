using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MotoGP.Domain.Strategies;
using MotoGP.Contracts.Models;
using MotoGP.Infrastructure.Services;

namespace MotoGP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RacesController : ControllerBase
{
    private readonly IRaceRepository _raceRepository;
    private readonly IMapper _mapper;
    private readonly IRaceDurationCalculator _durationCalculator;
    private readonly ILogger<RacesController> _logger;

    public RacesController(IRaceRepository raceRepository, IMapper mapper, IRaceDurationCalculator durationCalculator, ILogger<RacesController> logger)
    {
        _raceRepository = raceRepository;
        _mapper = mapper;
        _durationCalculator = durationCalculator;
        _logger = logger;
    }

    /// <summary>
    /// Returns all races, sorted on Date.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RaceListDto>>> GetRaces()
    {
        var races = await _raceRepository.GetRacesAsync();

        return Ok(_mapper.Map<IEnumerable<RaceListDto>>(races));
    }

    /// <summary>
    /// Returns a single race by its id.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<RaceDetailsDto>> GetRace(int id)
    {
        var race = await _raceRepository.GetRaceAsync(id);

        if (race is null)
        {
            _logger.LogWarning("Race with Id {RaceId} wasn't found.", id);
            return NotFound();
        }

        var raceDto = _mapper.Map<RaceDetailsDto>(race);
        raceDto.Duration = _durationCalculator.CalculateDuration(race);

        return Ok(raceDto);
    }
}
