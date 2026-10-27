using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using MotoGP.Api.Controllers;

namespace MotoGP.Api.Tests.Controllers;

/// <summary>
/// Verifies the ch.11 exercise brief's security requirements: GetTickets (read) and
/// PatchTicket (write, marks a ticket as paid) must both require a valid JWT bearer token,
/// while the other Tickets endpoints stay public.
/// </summary>
[TestClass]
public class TicketsControllerAuthorizationTests
{
    private static MethodInfo GetMethod(string name) =>
        typeof(TicketsController).GetMethod(name) ?? throw new InvalidOperationException($"Method {name} not found.");

    [TestMethod]
    public void GetTickets_HasAuthorizeAttribute()
    {
        var method = GetMethod(nameof(TicketsController.GetTickets));

        Assert.IsTrue(method.GetCustomAttributes<AuthorizeAttribute>().Any());
    }

    [TestMethod]
    public void PatchTicket_HasAuthorizeAttribute()
    {
        var method = GetMethod(nameof(TicketsController.PatchTicket));

        Assert.IsTrue(method.GetCustomAttributes<AuthorizeAttribute>().Any());
    }

    [TestMethod]
    public void GetTicket_HasNoAuthorizeAttribute()
    {
        var method = GetMethod(nameof(TicketsController.GetTicket));

        Assert.IsFalse(method.GetCustomAttributes<AuthorizeAttribute>().Any());
    }

    [TestMethod]
    public void CreateTicket_HasNoAuthorizeAttribute()
    {
        var method = GetMethod(nameof(TicketsController.CreateTicket));

        Assert.IsFalse(method.GetCustomAttributes<AuthorizeAttribute>().Any());
    }
}
