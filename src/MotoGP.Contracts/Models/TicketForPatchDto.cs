using System.ComponentModel.DataAnnotations;

namespace MotoGP.Contracts.Models;

/// <summary>Partial update contract - only Paid can be patched.</summary>
public class TicketForPatchDto
{
    [Required(ErrorMessage = "{0} is required.")]
    public bool? Paid { get; set; }
}
