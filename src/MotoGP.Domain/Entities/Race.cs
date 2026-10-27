using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotoGP.Domain.Entities;

public class Race
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Place { get; set; }

    [Required]
    public string Description { get; set; }

    public int Length { get; set; }

    public DateTime Date { get; set; }

    public int X { get; set; }

    public int Y { get; set; }

    /// <summary>Estimated number of laps in the race (used by <c>LapsBasedCalculator</c>).</summary>
    public int LapCount { get; set; }

    /// <summary>Estimated average speed in km/h (used by <c>DistanceBasedCalculator</c>).</summary>
    public double AvgSpeed { get; set; }

    /// <summary>Estimated time in seconds of one lap (used by <c>LapsBasedCalculator</c>).</summary>
    public double LapTime { get; set; }

    public ICollection<Ticket> Tickets { get; set; } = [];

    public Race(string name)
    {
        Name = name;
        Place = string.Empty;
        Description = string.Empty;
    }
}
