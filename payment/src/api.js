import axios from "axios";

const API_BASE = "https://localhost:7257/api";

const api = axios.create({
  baseURL: API_BASE,
});

// Add a request interceptor to attach token if present
api.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export default api;
