"use client";
import { useQuery } from "@tanstack/react-query";
import { useState } from "react";
import axios from "axios";
import useApi from "@/app/Hooks/useApi";
import useToast from "@/app/Hooks/useToast";
import LoadingSpin from "@/app/Components/Ui/LoadingSpin";
import "../../SCSS/Login.scss";
import Link from "next/link";
import { useMutation } from "@tanstack/react-query";
export default function LoginPage() {
  const [formData, setFormData] = useState({
    email: "",
    password: "",
  });
  const [remmember, setRemember] = useState(true);

  const logIn = useMutation({
    mutationFn: async () => {
      const res = await axios.post(`${useApi}/User/Login`, formData);
      return res.data;
    },
    onSuccess: (data) => {
      useToast({
        status: "success",
        description: "Logged In Successfully",
      });
      if (remmember) localStorage.setItem("token", data);
      else sessionStorage.setItem("token", data);
    },
    onError: (error: any) => {
      console.log(error);
      useToast({
        status: "error",
        description: error.response.data.Error[0] || "error occured",
      });
    },
  });
  return (
    <div id="LoginPage" className="flex LoginSigninPage">
      <div
        id="rightSideForm"
        className="w-1/2 h-full flex items-center justify-center"
      >
        <div id="linenb1"></div> <div id="linenb2"></div>
        <div className="w-80">
          <div className="w-full  font-sans text-3xl">
            {" "}
            <h1>Login</h1>
          </div>
          <br />
          <label
            htmlFor="Email"
            className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
          >
            Email
          </label>
          <input
            type="text"
            id="Email"
            className="  text-gray-900 text-sm border-b-2 border-black dark:border-gray-600 block w-full p-2.5   dark:placeholder-gray-400 dark:text-white "
            placeholder="Email@example.com"
            onChange={(e) =>
              setFormData({ ...formData, email: e.target.value })
            }
            disabled={logIn.isPending}
            required
          />
          <br />
          <label
            htmlFor="Password"
            className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
          >
            Password
          </label>
          <input
            type="password"
            id="Password"
            className="  text-gray-900 text-sm border-b-2 border-black dark:border-gray-600 block w-full p-2.5   dark:placeholder-gray-400 dark:text-white"
            // className="bg-gray-50 border  text-gray-900 text-sm rounded-lg   block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white "
            placeholder="********"
            onChange={(e) =>
              setFormData({ ...formData, password: e.target.value })
            }
            required
            disabled={logIn.isPending}
          />
          <br />
          <br />{" "}
          <input
            id="remember"
            checked={remmember}
            type="checkbox"
            onChange={(e) => setRemember(!remmember)}
          ></input>
          <label htmlFor="remember">Remember me</label>
          <br /> <br />
          <button
            className="border-2 border-black-600 rounded-lg w-16 h-10 bg-gray-600 text-gray-900 text-sm dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white"
            onClick={() => {
              logIn.mutate();
            }}
            disabled={logIn.isPending}
          >
            {logIn.isPending ? <LoadingSpin /> : <p>Login</p>}
          </button>
          <br /> <br />
          <hr
            style={{ borderWidth: "1px" }}
            className="border-black dark:border-gray-600 "
          />{" "}
          <br />
          <p>
            Don't have an account? <Link href="/Signup">Signup</Link>
          </p>
        </div>
      </div>
      <div id="leftSideImage" className="w-1/2 bg-cover bg-center">
        <div id="leftTitle">
          <b className="text-2xl font-serif">LebWork</b>
        </div>
      </div>
    </div>
  );
}
