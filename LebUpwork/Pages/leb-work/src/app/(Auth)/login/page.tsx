"use client";
import { useQuery } from "@tanstack/react-query";
import { useState } from "react";
import axios from "axios";
import useApi from "@/app/Hooks/useApi";
import LoadingSpin from "@/app/Components/Ui/LoadingSpin";
export default function LoginPage() {
  const [formData, setFormData] = useState({
    email: "",
    password: "",
  });
  const submitData = () => {
    refetch();
  };
  const { data, isLoading, isError, refetch } = useQuery({
    enabled: false,
    queryKey: ["Login"],
    queryFn: async () => {
      const { data } = await axios.post(`${useApi}/User/Login`, formData);
      return data;
    },
  });
  return (
    <div id="LoginPage">
      <div id="leftSideImage"></div>
      <div id="rightSideForm">
        <h1>Login</h1>
        <label htmlFor="Email">Email</label>
        <br />
        <input
          id="Email"
          onChange={(e) => setFormData({ ...formData, email: e.target.value })}
        ></input>{" "}
        <br />
        <label htmlFor="Password">Password</label> <br />
        <input
          id="Password"
          onChange={(e) =>
            setFormData({ ...formData, password: e.target.value })
          }
        ></input>{" "}
        <br />
        {isLoading ? (
          <LoadingSpin />
        ) : (
          <button onClick={submitData}>Login</button>
        )}
      </div>
    </div>
  );
}
