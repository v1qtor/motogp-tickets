# MotoGP Tickets

A full-stack MotoGP ticket reservation system: a layered ASP.NET Core Web API and a React
frontend for browsing races and booking tickets.

```
api/      MotoGP.Api — ASP.NET Core Web API (EF Core, AutoMapper, Auth0 JWT, MSTest)
client/   MotoGP Tickets — React (Vite) frontend consuming the API
```

See [`api/README.md`](api/README.md) and [`client/README.md`](client/README.md) for details on
each half.

## Architecture

**api/** is a 4-layer ASP.NET Core API with the Repository pattern:

```
api/
  src/
    MotoGP.Api/              # Controllers, Program.cs, AutoMapper profiles
    MotoGP.Contracts/        # DTOs only
    MotoGP.Domain/           # POCO entities, pagination helper, Strategy-pattern interfaces
    MotoGP.Infrastructure/   # EF Core DbContext, Migrations, Repositories, Seeders
  tests/
    MotoGP.Api.Tests/        # MSTest unit tests across all layers
```

**client/** is a Vite/React app that talks to the API over Axios, with the base URL driven by
`VITE_API_BASE_URL` so it can point at either a local or a deployed API.

## Running both locally

1. **API** — from `api/src/MotoGP.Api`:
   ```bash
   dotnet run
   ```
   Runs on `http://localhost:5130` / `https://localhost:7138` (Swagger at `/swagger`). Uses SQL
   Server LocalDB, migrated and seeded automatically on first run.

2. **Client** — from `client/`:
   ```bash
   npm install
   npm run dev
   ```
   Runs on `http://localhost:5177` by default and expects the API at `http://localhost:5130`
   (see `client/.env.development`). The API's CORS policy allows this origin.

3. Open the client URL Vite prints, browse races, and place a ticket order.

Auth0 `Authority`/`Audience` in the API's `appsettings.Development.json` are placeholders —
endpoints that require a JWT (the admin ticket list and marking a ticket paid) return `401`
until pointed at a real Auth0 tenant. Placing an order is public and works end-to-end without
any auth setup.

## Tests

```bash
cd api
dotnet test
```

## Tech stack

ASP.NET Core · Entity Framework Core · SQL Server · AutoMapper · Auth0 (JWT) · MSTest · Moq ·
React · Vite · Axios
