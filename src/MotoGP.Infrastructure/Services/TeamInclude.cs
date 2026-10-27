namespace MotoGP.Infrastructure.Services;

/// <summary>Controls which navigation property, if any, <see cref="ITeamRepository.GetTeamAsync"/> eager-loads.</summary>
public enum TeamInclude
{
    None,
    Riders,
    Sponsors
}
