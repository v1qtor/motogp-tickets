using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using MotoGP.Api.Configuration;
using MotoGP.Domain.Strategies;
using MotoGP.Infrastructure.DbContexts;
using MotoGP.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS (ch.10/ch.11): the React front end (Vite dev server) runs on a different origin.
// X-Pagination must be explicitly exposed - custom response headers are not readable from
// JavaScript across origins otherwise, even though the browser does receive them.
// Ports 5173-5179 cover the react-mini-projects workspace (01-components .. weather-auth-demo)
// so several of those mini-projects can run their dev servers side by side against this API.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins(
                "http://localhost:5173",
                "http://localhost:5174",
                "http://localhost:5175",
                "http://localhost:5176",
                "http://localhost:5177",
                "http://localhost:5178",
                "http://localhost:5179")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithExposedHeaders("X-Pagination"));
});

// EF Core / SQL Server LocalDB
builder.Services.AddDbContext<MotoGPContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MotoGPDBConnectionString")));

// AutoMapper - license key is stored via `dotnet user-secrets` (never committed to source control).
// The mapping profiles now live in this project's own MappingProfiles folder, so the profiles are
// discovered from the API's own assembly.
builder.Services.AddAutoMapper(cfg =>
{
    cfg.LicenseKey = builder.Configuration["AutoMapper:LicenseKey"];
}, typeof(Program).Assembly);

// Strategy Pattern (ch.8): swap to DistanceBasedCalculator to switch the calculation
// method without touching RacesController - only this registration needs to change.
builder.Services.AddScoped<IRaceDurationCalculator, LapsBasedCalculator>();

// Repository Pattern (ch.9): Scoped lifetime - one repository instance per request,
// matching the Scoped MotoGPContext it wraps.
builder.Services.AddScoped<IRiderRepository, RiderRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IRaceRepository, RaceRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

// Auth0 JWT bearer authentication (ch.11). Authority/Audience are read from configuration
// (Options pattern, ch.12) rather than hardcoded - see appsettings.Development.json for the
// placeholder values that must be replaced with a real Auth0 tenant/API application.
builder.Services.Configure<Auth0Options>(builder.Configuration.GetSection(Auth0Options.SectionName));
var auth0Options = builder.Configuration.GetSection(Auth0Options.SectionName).Get<Auth0Options>() ?? new Auth0Options();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = auth0Options.Authority;
        options.Audience = auth0Options.Audience;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

// Order matters: UseAuthentication() must run before UseAuthorization() so the JWT bearer
// handler gets a chance to populate the request's identity before [Authorize] checks it
// (the KMI.API reference sample this pattern is based on omits this - a known gotcha, ch.11).
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
