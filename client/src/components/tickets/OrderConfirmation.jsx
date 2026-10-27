// OrderConfirmation — presentational component, adapted from the ch.6
// starter `order_confirmation.jsx`. The <strong> placeholders are now
// filled with real fields from the TicketDetailsDto returned by
// POST /api/tickets, and "Place Another Order" resets via onReset.
function OrderConfirmation({ order, onReset }) {
  const orderDate = new Date(order.orderDate).toLocaleDateString();

  return (
    <div className="alert alert-success">
      <h4>Order Confirmed!</h4>
      <ul className="mt-4">
        <li>
          Thank you <strong>{order.name}</strong> for ordering tickets for the race in{" "}
          <strong>{order.raceName}</strong> on <strong>{order.shortRaceDate}</strong>.
        </li>
        <li>
          Your booking is registered on <strong>{orderDate}</strong>. We will send the{" "}
          <strong>{order.number}</strong> tickets as soon as possible.
        </li>
      </ul>
      <p>Your order has been placed successfully!</p>
      <button className="btn btn-success mt-3" onClick={onReset}>
        Place Another Order
      </button>
    </div>
  );
}

export default OrderConfirmation;
