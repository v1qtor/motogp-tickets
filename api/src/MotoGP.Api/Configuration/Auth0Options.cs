namespace MotoGP.Api.Configuration;

/// <summary>
/// Options-pattern binding for the "Auth0" configuration section (see appsettings.Development.json).
/// Bound once in Program.cs via <c>builder.Services.Configure&lt;Auth0Options&gt;(...)</c> and read
/// back to configure JWT bearer authentication.
/// </summary>
public class Auth0Options
{
    public const string SectionName = "Auth0";

    /// <summary>The Auth0 tenant domain, with "https://" prepended (e.g. "https://dev-xxxx.us.auth0.com").</summary>
    public string Authority { get; set; } = string.Empty;

    /// <summary>The API application's own Identifier, as registered in Auth0 (e.g. the API's base URL).</summary>
    public string Audience { get; set; } = string.Empty;
}
