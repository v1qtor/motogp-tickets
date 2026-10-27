namespace MotoGP.Contracts.Models;

/// <summary>
/// Details of one team plus its sponsors, returned by GetSponsorsForTeam.
/// </summary>
public class TeamWithSponsorsDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Logo { get; set; }
    public List<SponsorDto> Sponsors { get; set; } = [];
}
