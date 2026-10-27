# MotoGP Tickets — client

React (Vite) frontend for browsing races and reserving MotoGP tickets against the
[`MotoGP.Api`](../api) backend in this repo.

## What it does

- **Order Tickets** — a visitor books race tickets through a multi-field form (name, email,
  address, country, race, ticket count), submits via `POST /api/tickets`, and sees a
  confirmation built from the API's response.
- **Tickets (admin)** — a filterable, paginated ticket list (search, country/race filters,
  payment-status toggle) with a "Pay" action per unpaid row (`PATCH /api/tickets/{id}`).
  Pagination reads the `X-Pagination` response header.

### Auth note

`GET /api/tickets` and `PATCH /api/tickets/{id}` require a valid Auth0 JWT bearer token on the
API side. This app has no login flow, so with the API's placeholder Auth0 config the **Tickets**
screen's list load and **Pay** action correctly show a handled `401` error state rather than
crashing. Placing an order (`POST /api/tickets`) is public and works end-to-end.

## Configuration

The API base URL is read from `VITE_API_BASE_URL` (see `.env.development` / `.env.example`),
defaulting to the local API's HTTP profile:

```
VITE_API_BASE_URL=http://localhost:5130/api/
```

Point this at a deployed API URL once the backend is hosted somewhere other than localhost.

## Run it

```bash
npm install
npm run dev
```

Make sure [`MotoGP.Api`](../api) is running first (`dotnet run` from `api/src/MotoGP.Api`).

## Build

```bash
npm run build
```
