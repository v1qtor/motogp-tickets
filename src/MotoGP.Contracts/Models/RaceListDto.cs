namespace MotoGP.Contracts.Models;

/// <summary>
/// Reduced-payload race representation for list views — same fields as
/// <see cref="RaceDetailsDto"/> except <c>Description</c> and <c>TrackLength</c>.
/// </summary>
public class RaceListDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Place { get; set; }

    /// <summary>Date formatted as "dd/MM".</summary>
    public required string ShortDate { get; set; }

    /// <summary>Month number (1-12) of the race date.</summary>
    public int Month { get; set; }

    public int X { get; set; }
    public int Y { get; set; }
}
