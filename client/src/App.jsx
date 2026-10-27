import { BrowserRouter, NavLink, Route, Routes } from "react-router-dom";
import OrderScreen from "./screens/OrderScreen";
import TicketsScreen from "./screens/TicketsScreen";

function Home() {
  return (
    <div className="page">
      <h2>MotoGP Ticket Reservation</h2>
      <p>
        ch.6 (Ticket Reservation CUD) + ch.10 (Filtering, searching &amp; paging), adapted from
        the course's starter <code>tickets/</code> and <code>ui/</code> resource files.
      </p>
      <ul>
        <li>
          <NavLink to="/order_tickets">Order Tickets</NavLink> — place a new reservation
          (public <code>POST /api/tickets</code>).
        </li>
        <li>
          <NavLink to="/list_tickets">Tickets</NavLink> — filter/search/paginate the reservation
          list and mark tickets as paid (both require an Auth0 token — see the note on that
          screen).
        </li>
      </ul>
    </div>
  );
}

function App() {
  return (
    <BrowserRouter>
      <nav className="navbar">
        <NavLink to="/" end className={({ isActive }) => (isActive ? "active" : undefined)}>
          Home
        </NavLink>
        <NavLink to="/order_tickets" className={({ isActive }) => (isActive ? "active" : undefined)}>
          Order Tickets
        </NavLink>
        <NavLink to="/list_tickets" className={({ isActive }) => (isActive ? "active" : undefined)}>
          Tickets
        </NavLink>
      </nav>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/order_tickets" element={<OrderScreen />} />
        <Route path="/list_tickets" element={<TicketsScreen />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
