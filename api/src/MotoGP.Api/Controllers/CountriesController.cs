using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MotoGP.Contracts.Models;
using MotoGP.Infrastructure.Services;

namespace MotoGP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CountriesController> _logger;

    public CountriesController(ICountryRepository countryRepository, IMapper mapper, ILogger<CountriesController> logger)
    {
        _countryRepository = countryRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Returns all countries, sorted by Name. Pass onlyWithTickets=true to return only
    /// countries that have at least one sold ticket.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CountryListDto>>> GetCountries(bool onlyWithTickets = false)
    {
        var countries = await _countryRepository.GetCountriesAsync(onlyWithTickets);

        _logger.LogInformation("Returned {CountryCount} countries (onlyWithTickets={OnlyWithTickets}).", countries.Count(), onlyWithTickets);

        return Ok(_mapper.Map<IEnumerable<CountryListDto>>(countries));
    }
}
