using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MotoGP.Contracts.Models;
using MotoGP.Infrastructure.Services;

namespace MotoGP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RidersController : ControllerBase
{
    private readonly IRiderRepository _riderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<RidersController> _logger;

    public RidersController(IRiderRepository riderRepository, IMapper mapper, ILogger<RidersController> logger)
    {
        _riderRepository = riderRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Returns all riders, sorted on Number, including the team name.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RiderListDto>>> GetRiders()
    {
        var riders = await _riderRepository.GetRidersAsync();

        _logger.LogInformation("Returned {RiderCount} riders.", riders.Count());

        return Ok(_mapper.Map<IEnumerable<RiderListDto>>(riders));
    }
}
