import { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import CountriesService from "../api/countries_service";
import RacesService from "../api/races_service";
import TicketsService from "../api/tickets_service";
import OrderForm from "../components/tickets/OrderForm";
import OrderConfirmation from "../components/tickets/OrderConfirmation";

// OrderScreen — container, adapted from the ch.6 starter `order_screen.jsx`.
// Completed per the exercise docx:
//  - fetch countries + races once (useEffect, [] dependency array) into
//    state for the OrderForm's dropdown/radio list
//  - implement handleOrder: POST the order, populate orderConfirmation
//    with the response, and conditionally render OrderConfirmation vs
//    OrderForm depending on whether an order has been placed
function OrderScreen() {
  const navigate = useNavigate();

  const [countries, setCountries] = useState([]);
  const [races, setRaces] = useState([]);
  const [orderConfirmation, setOrderConfirmation] = useState(null);
  const [error, setError] = useState(null);

  useEffect(() => {
    CountriesService.getAll()
      .then((result) => setCountries(result.data))
      .catch((err) => console.error("Error fetching countries:", err));

    RacesService.getAll()
      .then((result) => setRaces(result.data))
      .catch((err) => console.error("Error fetching races:", err));
  }, []);

  async function handleOrder(order) {
    try {
      setError(null);
      const result = await TicketsService.placeOrder(order);
      setOrderConfirmation(result.data);
    } catch (err) {
      console.error("Error placing order:", err);
      setError(
        err.response?.data
          ? JSON.stringify(err.response.data)
          : "Could not place the order. Is the API reachable at the configured VITE_API_BASE_URL?"
      );
    }
  }

  return (
    <div className="page">
      <h2>Order Tickets</h2>

      {error && <p className="error">{error}</p>}

      {orderConfirmation ? (
        <OrderConfirmation order={orderConfirmation} onReset={() => setOrderConfirmation(null)} />
      ) : (
        <OrderForm countries={countries} races={races} onSubmit={handleOrder} />
      )}

      <p>
        <Link onClick={() => navigate(-1)}>Back</Link>
      </p>
    </div>
  );
}

export default OrderScreen;
