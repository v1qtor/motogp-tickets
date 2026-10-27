import apiClient from "./api_client";

const CountriesService = {
  getAll() {
    return apiClient.get("/countries");
  },
  // ch.10 exercise: onlyWithTickets=true returns only countries with at least one sold ticket.
  getAllWithTickets() {
    return apiClient.get("/countries", { params: { onlyWithTickets: true } });
  },
};

export default CountriesService;
