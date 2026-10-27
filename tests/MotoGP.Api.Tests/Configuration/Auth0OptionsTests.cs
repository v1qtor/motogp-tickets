using Microsoft.Extensions.Configuration;
using MotoGP.Api.Configuration;

namespace MotoGP.Api.Tests.Configuration;

[TestClass]
public class Auth0OptionsTests
{
    [TestMethod]
    public void Get_BindsAuthorityAndAudienceFromAuth0Section()
    {
        var inMemorySettings = new Dictionary<string, string?>
        {
            ["Auth0:Authority"] = "https://dev-example.us.auth0.com",
            ["Auth0:Audience"] = "https://localhost:7000"
        };
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();

        var options = configuration.GetSection(Auth0Options.SectionName).Get<Auth0Options>();

        Assert.IsNotNull(options);
        Assert.AreEqual("https://dev-example.us.auth0.com", options.Authority);
        Assert.AreEqual("https://localhost:7000", options.Audience);
    }

    [TestMethod]
    public void Get_MissingSection_YieldsDefaultEmptyValues()
    {
        var configuration = new ConfigurationBuilder().Build();

        var options = new Auth0Options();
        configuration.GetSection(Auth0Options.SectionName).Bind(options);

        Assert.AreEqual(string.Empty, options.Authority);
        Assert.AreEqual(string.Empty, options.Audience);
    }
}
