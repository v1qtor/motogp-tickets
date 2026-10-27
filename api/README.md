# MotoGP.Api

A layered ASP.NET Core Web API for managing MotoGP teams, riders, races, and ticket
reservations — built with EF Core, AutoMapper, and Auth0 JWT authentication.

## Architecture

4-layer architecture with the Repository pattern:

```
src/
  MotoGP.Api/              # Controllers, Program.cs (composition root), AutoMapper profiles
  MotoGP.Contracts/        # DTOs only
  MotoGP.Domain/           # POCO entities, pagination helper, Strategy-pattern interfaces
  MotoGP.Infrastructure/   # EF Core DbContext, Migrations, Repositories, Seeders
tests/
  MotoGP.Api.Tests/        # MSTest unit tests across all layers
```

- `Api -> Contracts, Domain, Infrastructure`; `Infrastructure -> Domain`; `Contracts`/`Domain`
  have no project references.
- Repositories return domain entities; controllers inject the repository + `IMapper` +
  `ILogger<TController>` and stay thin (validate → call repository → map → return).
- Pagination/filtering/searching is handled in repository methods and surfaced via the
  `X-Pagination` response header.
- Strategy pattern for race duration: `IRaceDurationCalculator` swaps between
  laps-based and distance-based calculation via DI, with no controller changes.
- Auth0 JWT bearer authentication on write/sensitive endpoints.

## Domain

Teams, sponsors (with a `TeamSponsor` join entity), riders, races, countries, and ticket
reservations, seeded with realistic sample data (190 countries, 7 teams, 6 sponsors,
12 riders, 19 races, 20 tickets).

## Endpoints

| Resource | Routes |
|---|---|
| Teams | `GET /api/teams`, `GET /api/teams/{id}/riders`, `GET /api/teams/{id}/sponsors` |
| Riders | `GET /api/riders` |
| Races | `GET /api/races`, `GET /api/races/{id}` (includes calculated `duration`) |
| Countries | `GET /api/countries?onlyWithTickets=` |
| Tickets | `GET /api/tickets` 🔒, `GET /api/tickets/{id}`, `POST /api/tickets`, `PATCH /api/tickets/{id}` 🔒 |
| Seed | `POST /api/seed/tickets` (dev utility, generates extra fake tickets via Bogus) |

🔒 = requires a valid Auth0 JWT bearer token.

## Running locally

```bash
cd src/MotoGP.Api
dotnet run
```

- HTTP: `http://localhost:5130`
- HTTPS: `https://localhost:7138`
- Swagger UI: `https://localhost:7138/swagger`
- Database: SQL Server LocalDB, created automatically from migrations on first run. If it
  doesn't auto-apply: `dotnet ef database update` from `src/MotoGP.Api`.

Auth0 `Authority`/`Audience` in `appsettings.Development.json` are placeholders — 🔒 endpoints
return `401` until you point them at a real Auth0 tenant.

## Tests

```bash
dotnet test
```

MSTest, covering controllers, repositories, mapping profiles, and strategies.

## Tech stack

ASP.NET Core · Entity Framework Core · SQL Server · AutoMapper · Auth0 (JWT) · MSTest · Moq
