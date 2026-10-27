import apiClient from "./api_client";

const RacesService = {
  getAll() {
    return apiClient.get("/races");
  },
};

export default RacesService;
