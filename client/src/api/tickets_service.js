import apiClient from "./api_client";

// TicketsService — one method per TicketsController endpoint (ch.6 + ch.10).
// getAll/registerPayment require a bearer token in the real MotoGP.Api
// because GetTickets/PatchTicket are [Authorize]d (ch.11) — see README.
const TicketsService = {
  // ch.10: filters/searches/pages via query-string params, Axios serializes
  // the `params` object automatically.
  getAll(params) {
    return apiClient.get("/tickets", { params });
  },

  placeOrder(orderData) {
    return apiClient.post("/tickets", orderData);
  },

  registerPayment(ticketId) {
    const patchData = { paid: true };
    return apiClient.patch(`/tickets/${ticketId}`, patchData);
  },
};

export default TicketsService;
