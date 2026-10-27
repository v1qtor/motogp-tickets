using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotoGP.Domain.Entities;

/// <summary>
/// Join/association entity for the Team &lt;-&gt; Sponsor many-to-many relationship.
/// </summary>
public class TeamSponsor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey(nameof(Team))]
    public int TeamId { get; set; }

    public Team Team { get; set; } = null!;

    [ForeignKey(nameof(Sponsor))]
    public int SponsorId { get; set; }

    public Sponsor Sponsor { get; set; } = null!;
}
