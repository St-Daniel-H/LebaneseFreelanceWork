import axios from "axios";
import useApi from "./useApi";

const token = localStorage.getItem("token") || sessionStorage.getItem("token");

const axiosInstance = axios.create({
  baseURL: useApi,
  headers: {
    "Content-Type": "application/json",
    Authorization: `Bearer ${token}`,
  },
});

export default axiosInstance;
