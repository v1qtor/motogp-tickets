using MotoGP.Domain.Entities;

namespace MotoGP.Domain.Strategies;

/// <summary>
/// Strategy Pattern interface: encapsulates an interchangeable algorithm for estimating
/// how long a race will take, so the calculation method can be swapped via DI without
/// touching <see cref="MotoGP.Api.Controllers.RacesController"/>.
/// </summary>
public interface IRaceDurationCalculator
{
    TimeSpan CalculateDuration(Race race);
}
