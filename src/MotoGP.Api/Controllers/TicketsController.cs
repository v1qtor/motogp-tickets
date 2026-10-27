using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoGP.Contracts.Models;
using MotoGP.Infrastructure.Services;

namespace MotoGP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IRaceRepository _raceRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<TicketsController> _logger;

    public TicketsController(
        ITicketRepository ticketRepository,
        IRaceRepository raceRepository,
        ICountryRepository countryRepository,
        IMapper mapper,
        ILogger<TicketsController> logger)
    {
        _ticketRepository = ticketRepository;
        _raceRepository = raceRepository;
        _countryRepository = countryRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Returns tickets, sorted by OrderDate, filtered/searched/paged via query string parameters.
    /// Pagination metadata (total item count, total page count, page size, current page) is
    /// returned in the "X-Pagination" response header.
    /// </summary>
    /// <param name="searchQuery">Matches buyer name or email.</param>
    /// <param name="countryId">Only tickets for this country.</param>
    /// <param name="raceId">Only tickets for this race.</param>
    /// <param name="open">True = only unpaid tickets; false = all tickets.</param>
    /// <param name="pageNumber">1-based page number, defaults to the first page.</param>
    /// <param name="pageSize">Number of items per page, defaults to 10 (capped server-side).</param>
    /// <remarks>Secured per the ch.11 exercise brief - requires a valid Auth0-issued JWT bearer token.</remarks>
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketListDto>>> GetTickets(
        string? searchQuery = null,
        int? countryId = null,
        int? raceId = null,
        bool open = false,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var (tickets, paginationMetadata) = await _ticketRepository.GetTicketsAsync(
            searchQuery, countryId, raceId, open, pageNumber, pageSize);

        _logger.LogInformation(
            "Returned page {CurrentPage}/{TotalPageCount} of tickets ({TotalItemCount} total, searchQuery={SearchQuery}, countryId={CountryId}, raceId={RaceId}, open={Open}).",
            paginationMetadata.CurrentPage, paginationMetadata.TotalPageCount, paginationMetadata.TotalItemCount, searchQuery, countryId, raceId, open);

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

        return Ok(_mapper.Map<IEnumerable<TicketListDto>>(tickets));
    }

    /// <summary>
    /// Returns one ticket by Id.
    /// </summary>
    [HttpGet("{id}", Name = "GetTicket")]
    public async Task<ActionResult<TicketDetailsDto>> GetTicket(int id)
    {
        var ticket = await _ticketRepository.GetTicketAsync(id);

        if (ticket is null)
        {
            _logger.LogWarning("Ticket with Id {TicketId} wasn't found.", id);
            return NotFound();
        }

        return Ok(_mapper.Map<TicketDetailsDto>(ticket));
    }

    /// <summary>
    /// Places a new ticket reservation for a race.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<TicketDetailsDto>> CreateTicket(TicketForCreationDto ticket)
    {
        if (!await _countryRepository.CountryExistsAsync(ticket.CountryId!.Value))
        {
            _logger.LogWarning("CreateTicket rejected: Country with Id {CountryId} does not exist.", ticket.CountryId);
            return BadRequest($"Race/Country with Id {ticket.CountryId} does not exist.");
        }

        if (!await _raceRepository.RaceExistsAsync(ticket.RaceId!.Value))
        {
            _logger.LogWarning("CreateTicket rejected: Race with Id {RaceId} does not exist.", ticket.RaceId);
            return BadRequest($"Race/Country with Id {ticket.RaceId} does not exist.");
        }

        var ticketEntity = _mapper.Map<Domain.Entities.Ticket>(ticket);
        ticketEntity.OrderDate = DateTime.Now;
        ticketEntity.Paid = false;

        _ticketRepository.AddTicket(ticketEntity);
        await _ticketRepository.SaveChangesAsync();

        // The Race navigation property isn't populated automatically just because RaceId
        // was set - it must be explicitly (re)loaded before RaceName/ShortRaceDate can be mapped.
        await _ticketRepository.LoadRaceForTicketAsync(ticketEntity);

        var createdTicketToReturn = _mapper.Map<TicketDetailsDto>(ticketEntity);

        _logger.LogInformation("Created ticket {TicketId} for race {RaceId}, country {CountryId}.", createdTicketToReturn.Id, ticket.RaceId, ticket.CountryId);

        return CreatedAtRoute("GetTicket", new { id = createdTicketToReturn.Id }, createdTicketToReturn);
    }

    /// <summary>
    /// Marks a ticket as paid.
    /// </summary>
    /// <remarks>
    /// Secured per the ch.11 exercise brief: this write endpoint was found to be callable
    /// without a token even after GetTickets was secured - closing that gap is an explicit
    /// step in the brief (securing the read endpoint alone is not enough).
    /// </remarks>
    [Authorize]
    [HttpPatch("{id}")]
    public async Task<ActionResult> PatchTicket(int id, TicketForPatchDto ticket)
    {
        var ticketEntity = await _ticketRepository.GetTicketAsync(id);

        if (ticketEntity is null)
        {
            _logger.LogWarning("PatchTicket failed: ticket with Id {TicketId} wasn't found.", id);
            return NotFound();
        }

        _mapper.Map(ticket, ticketEntity);
        await _ticketRepository.SaveChangesAsync();

        _logger.LogInformation("Ticket {TicketId} payment status updated to Paid={Paid}.", id, ticketEntity.Paid);

        return NoContent();
    }
}
