// TicketsList / TicketItem — presentational, adapted from the ch.6
// starter `tickets_list.jsx`. TicketItem now renders real props (not
// literal placeholder strings) and conditionally renders a "Pay" button
// only when the ticket isn't paid yet.
function TicketItem({ ticket, onPay }) {
  return (
    <tr>
      <td>{ticket.shortOrderDate}</td>
      <td>{ticket.name}</td>
      <td>{ticket.raceName}</td>
      <td>{ticket.number}</td>
      <td>
        {ticket.paid ? (
          <span className="badge bg-success">Paid</span>
        ) : (
          <button className="btn btn-success btn-sm" onClick={() => onPay(ticket.id)}>
            Pay
          </button>
        )}
      </td>
    </tr>
  );
}

function TicketsList({ tickets, onPay }) {
  if (tickets.length === 0) {
    return <p className="hint">No tickets match the current filters.</p>;
  }

  return (
    <table className="table table-striped">
      <thead>
        <tr>
          <th scope="col">Order Date</th>
          <th scope="col">Name</th>
          <th scope="col">Race</th>
          <th scope="col">#Tickets</th>
          <th scope="col">Paid</th>
        </tr>
      </thead>
      <tbody>
        {tickets.map((ticket) => (
          <TicketItem key={ticket.id} ticket={ticket} onPay={onPay} />
        ))}
      </tbody>
    </table>
  );
}

export default TicketsList;
