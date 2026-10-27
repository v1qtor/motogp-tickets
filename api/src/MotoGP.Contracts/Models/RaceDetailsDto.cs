namespace MotoGP.Contracts.Models;

/// <summary>
/// Full race details. Deliberately does NOT include the raw <c>Date</c>/<c>Length</c>
/// fields — those are represented as <see cref="ShortDate"/>/<see cref="Month"/> and
/// <see cref="TrackLength"/> instead (see Chapter 5 exercise brief).
/// </summary>
public class RaceDetailsDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Place { get; set; }
    public required string Description { get; set; }

    /// <summary>Date formatted as "dd/MM".</summary>
    public required string ShortDate { get; set; }

    /// <summary>Month number (1-12) of the race date.</summary>
    public int Month { get; set; }

    /// <summary>Track length expressed in kilometers (Race.Length is in meters).</summary>
    public double TrackLength { get; set; }

    public int X { get; set; }
    public int Y { get; set; }

    /// <summary>
    /// Estimated total race duration, populated by <see cref="MotoGP.Api.Controllers.RacesController"/>
    /// via an injected <see cref="MotoGP.Domain.Strategies.IRaceDurationCalculator"/> (AutoMapper profiles
    /// cannot receive constructor-injected dependencies, so this cannot be set from a mapping expression).
    /// </summary>
    public TimeSpan Duration { get; set; }
}
