import { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import CountriesService from "../api/countries_service";
import RacesService from "../api/races_service";
import TicketsService from "../api/tickets_service";
import TicketsFilter from "../components/tickets/TicketsFilter";
import TicketsList from "../components/tickets/TicketsList";
import Pagination from "../components/ui/Pagination";

const DEFAULT_FILTERS = {
  searchQuery: null,
  countryId: null,
  raceId: null,
  open: false,
  pageNumber: 1,
  pageSize: 10,
};

// TicketsScreen — container, adapted from the ch.6 starter
// `tickets_screen.jsx` plus the ch.10 filtering/paging exercise.
//
// NOTE: GetTickets (and PatchTicket, used by the "Pay" button) are
// [Authorize]-protected on MotoGP.Api per the ch.11 exercise brief. This
// mini-project (ch.6/ch.10 scope) doesn't integrate Auth0, so with only
// placeholder Auth0 config these calls correctly come back 401 — that's
// demonstrated here as an error state rather than a crash. See
// react-mini-projects/weather-auth-demo for the full Auth0 + Bearer token
// flow that would make this screen work end-to-end.
function TicketsScreen() {
  const navigate = useNavigate();

  const [countries, setCountries] = useState([]);
  const [races, setRaces] = useState([]);
  const [tickets, setTickets] = useState([]);
  const [filters, setFilters] = useState(DEFAULT_FILTERS);
  const [pageCount, setPageCount] = useState(1);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  // Lookup data for the filter dropdowns — fetched once, not on every render.
  useEffect(() => {
    CountriesService.getAll()
      .then((result) => setCountries(result.data))
      .catch((err) => console.error("Error fetching countries:", err));

    RacesService.getAll()
      .then((result) => setRaces(result.data))
      .catch((err) => console.error("Error fetching races:", err));
  }, []);

  // Re-fetch whenever filters change (searchQuery/countryId/raceId/open/pageNumber/pageSize).
  useEffect(() => {
    let cancelled = false;

    async function getTickets() {
      try {
        setLoading(true);
        setError(null);
        const result = await TicketsService.getAll(filters);
        if (cancelled) return;
        setTickets(result.data);
        // Axios lower-cases response header names.
        const pagination = result.headers["x-pagination"];
        setPageCount(pagination ? JSON.parse(pagination).TotalPageCount : 1);
      } catch (err) {
        console.error("Error fetching tickets:", err);
        if (cancelled) return;
        if (err.response?.status === 401) {
          setError(
            "401 Unauthorized — GetTickets requires a valid Auth0 JWT bearer token (see weather-auth-demo for the full login flow)."
          );
        } else {
          setError("Could not load tickets. Is the API reachable at the configured VITE_API_BASE_URL?");
        }
      } finally {
        if (!cancelled) setLoading(false);
      }
    }

    getTickets();
    return () => {
      cancelled = true;
    };
  }, [filters]);

  async function handlePay(ticketId) {
    try {
      await TicketsService.registerPayment(ticketId);
      // Refresh the list so the ticket's paid status updates.
      const result = await TicketsService.getAll(filters);
      setTickets(result.data);
    } catch (err) {
      console.error("Error registering payment:", err);
      if (err.response?.status === 401) {
        setError("401 Unauthorized — PatchTicket requires a valid Auth0 JWT bearer token.");
      } else {
        setError("Could not register the payment.");
      }
    }
  }

  function handlePageChange(newPage) {
    setFilters({ ...filters, pageNumber: newPage });
  }

  return (
    <div className="page">
      <h2>Tickets</h2>

      <TicketsFilter filters={filters} onFilterChange={setFilters} countries={countries} races={races} />

      <Pagination currentPage={filters.pageNumber} totalPages={pageCount} onPageChange={handlePageChange} />

      {loading && <p className="hint">Loading tickets...</p>}
      {error && <p className="error">{error}</p>}
      {!loading && !error && <TicketsList tickets={tickets} onPay={handlePay} />}

      <p>
        <Link onClick={() => navigate(-1)}>Back</Link>
      </p>
    </div>
  );
}

export default TicketsScreen;
