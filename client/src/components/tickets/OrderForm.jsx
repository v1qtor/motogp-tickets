import { useState } from "react";

// OrderForm — presentational component, adapted from the ch.6 starter
// `order_form.jsx`. Completed per the exercise docx:
//  - added `email`/`address` to the controlled `order` state (the starter
//    only had name/countryId/raceId/number)
//  - populated the country <select> and race radio list from props
//    instead of an empty array
//  - implemented handleSubmit (preventDefault — SPA form, then hand the
//    order up to the parent via the `onSubmit` callback prop)
function OrderForm({ countries, races, onSubmit }) {
  const [order, setOrder] = useState({
    name: "",
    email: "",
    address: "",
    countryId: "",
    raceId: "",
    number: 1,
  });

  function handleChange(event) {
    const { name, value } = event.target;
    setOrder({
      ...order,
      [name]: value,
    });
  }

  function handleNumberChange(event) {
    setOrder({
      ...order,
      number: Number(event.target.value),
    });
  }

  function handleSubmit(event) {
    event.preventDefault();
    onSubmit({
      name: order.name,
      email: order.email,
      address: order.address,
      countryId: Number(order.countryId),
      raceId: Number(order.raceId),
      number: order.number,
    });
  }

  return (
    <form onSubmit={handleSubmit}>
      <div className="mb-3">
        <label className="form-label">Name</label>
        <input
          type="text"
          className="form-control"
          name="name"
          value={order.name}
          onChange={handleChange}
          required
        />
      </div>

      <div className="mb-3">
        <label className="form-label">Email</label>
        <input
          type="email"
          className="form-control"
          name="email"
          value={order.email}
          onChange={handleChange}
          required
        />
      </div>

      <div className="mb-3">
        <label className="form-label">Address</label>
        <input
          type="text"
          className="form-control"
          name="address"
          value={order.address}
          onChange={handleChange}
          required
        />
      </div>

      <div className="mb-3">
        <label className="form-label">Country</label>
        <select
          className="form-select"
          name="countryId"
          value={order.countryId}
          onChange={handleChange}
          required
        >
          <option value="">Select Country</option>
          {countries.map((country) => (
            <option key={country.id} value={country.id}>
              {country.name}
            </option>
          ))}
        </select>
      </div>

      <div className="mb-3">
        <label className="form-label">Race</label>
        <div className="row">
          {races.map((race) => (
            <div className="col-md-4 col-sm-6 col-12" key={race.id}>
              <div className="form-check">
                <input
                  className="form-check-input"
                  type="radio"
                  name="raceId"
                  value={race.id}
                  checked={String(order.raceId) === String(race.id)}
                  onChange={handleChange}
                  required
                />
                <label className="form-check-label">
                  {race.name} - {race.place}
                </label>
              </div>
            </div>
          ))}
        </div>
      </div>

      <div className="mb-3">
        <label className="form-label"># Tickets</label>
        <input
          type="number"
          className="form-control"
          name="number"
          min="1"
          max="10"
          value={order.number}
          onChange={handleNumberChange}
          required
        />
      </div>

      <button type="submit" className="btn btn-primary mb-4">
        Place Order
      </button>
    </form>
  );
}

export default OrderForm;
