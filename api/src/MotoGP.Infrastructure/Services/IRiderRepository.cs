using MotoGP.Domain.Entities;

namespace MotoGP.Infrastructure.Services;

public interface IRiderRepository
{
    /// <summary>Returns all riders, sorted on Number, including their Team.</summary>
    Task<IEnumerable<Rider>> GetRidersAsync();
}
