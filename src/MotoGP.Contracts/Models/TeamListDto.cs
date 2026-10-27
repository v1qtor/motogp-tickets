namespace MotoGP.Contracts.Models;

/// <summary>
/// Team list view (renamed from Chapter 4's TeamDto), including all riders in the team.
/// </summary>
public class TeamListDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Logo { get; set; }
    public List<RiderListDto> Riders { get; set; } = [];
}
