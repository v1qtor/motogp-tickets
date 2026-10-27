// TicketsFilter — adapted from the ch.10 resource `tickets_filter.jsx`
// almost verbatim: purely presentational, receives filters/onFilterChange
// /countries/races via props. Every change resets pageNumber back to 1
// (explicitly called out as a gotcha in the ch.10 notes).
function TicketsFilter({ filters, onFilterChange, countries, races }) {
  const handleChange = (e) => {
    const { name, value } = e.target;
    onFilterChange({ ...filters, [name]: value || null, pageNumber: 1 });
  };

  const toggleOpen = () => {
    onFilterChange({ ...filters, open: filters.open === true ? false : true, pageNumber: 1 });
  };

  return (
    <div className="alert alert-secondary">
      <form>
        <div className="row mb-3 align-items-end">
          <div className="col-md-3">
            <label className="form-label">Search</label>
            <input
              type="text"
              className="form-control"
              name="searchQuery"
              placeholder="Search by name or email..."
              value={filters.searchQuery || ""}
              onChange={handleChange}
            />
          </div>

          <div className="col-md-3">
            <label className="form-label">Country</label>
            <select
              className="form-select"
              name="countryId"
              value={filters.countryId || ""}
              onChange={handleChange}
            >
              <option value="">All Countries</option>
              {countries.map((country) => (
                <option key={country.id} value={country.id}>
                  {country.name}
                </option>
              ))}
            </select>
          </div>

          <div className="col-md-3">
            <label className="form-label">Race</label>
            <select
              className="form-select"
              name="raceId"
              value={filters.raceId || ""}
              onChange={handleChange}
            >
              <option value="">All Races</option>
              {races.map((race) => (
                <option key={race.id} value={race.id}>
                  {race.name} - {race.place}
                </option>
              ))}
            </select>
          </div>

          <div className="col-md-3">
            <label className="form-label d-block">Payment Status</label>
            <button
              type="button"
              className={`btn ${filters.open ? "btn-success" : "btn-secondary"} w-100`}
              onClick={toggleOpen}
            >
              {filters.open ? "Only Open Payments" : "All Tickets"}
            </button>
          </div>
        </div>
      </form>
    </div>
  );
}

export default TicketsFilter;
