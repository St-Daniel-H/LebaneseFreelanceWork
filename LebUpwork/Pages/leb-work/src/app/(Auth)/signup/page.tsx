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
export default function SignupPage() {
  const [formData, setFormData] = useState({
    Email: "",
    Password: "",
    FirstName: "",
    LastName: "",
  });
  const [errors, setErrors] = useState({
    emailError: "",
    passwordError: "",
    firstNameError: "",
    lastNameError: "",
  });
  const logIn = useMutation({
    mutationFn: async () => {
      const res = await axios.post(`${useApi}/User/Signup`, formData);
      return res.data;
    },
    onSuccess: (data) => {
      useToast({
        status: "success",
        description: "Signed up Successfully",
      });
      console.log(data);
    },
    onError: (error: any) => {
      console.log(error);
      const errorsReceived = error.response.data.$values;
      if (errorsReceived) {
        const newErrors = {
          emailError: "",
          passwordError: "",
          firstNameError: "",
          lastNameError: "",
        };
        errorsReceived.forEach((element: any) => {
          switch (element.propertyName) {
            case "Email":
              if (newErrors.emailError === "") {
                newErrors.emailError = element.errorMessage;
              }
              break;
            case "Password":
              if (newErrors.passwordError === "") {
                newErrors.passwordError = element.errorMessage;
              }
              break;
            case "FirstName":
              if (newErrors.firstNameError === "") {
                newErrors.firstNameError = element.errorMessage;
              }
              break;
            case "LastName":
              if (newErrors.lastNameError === "") {
                newErrors.lastNameError = element.errorMessage;
              }
              break;
            default:
              break;
          }
        });
        setErrors(newErrors);
      }

      useToast({
        status: "error",
        description: "error occured",
      });
    },
  });
  return (
    <div id="SignupPage" className="flex LoginSigninPage">
      <div
        id="rightSideForm"
        className="w-1/2 h-full flex items-center justify-center"
      >
        <div id="linenb1"></div> <div id="linenb2"></div>
        <div className="w-80">
          <div className="w-full  font-sans text-3xl">
            {" "}
            <h1>Signup</h1>
          </div>
          <br />
          <label
            htmlFor="fn"
            className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
          >
            First Name{" "}
            <span className="errorLabel">&nbsp;*{errors.firstNameError}</span>
          </label>
          <input
            type="text"
            id="fn"
            className="  text-gray-900 text-sm border-b-2 border-black dark:border-gray-600 block w-full p-2.5   dark:placeholder-gray-400 dark:text-white "
            placeholder="John"
            onChange={(e) =>
              setFormData({ ...formData, FirstName: e.target.value })
            }
            disabled={logIn.isPending}
            required
          />
          <br />
          <label
            htmlFor="ln"
            className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
          >
            Last Name
            <span className="errorLabel">&nbsp;*{errors.lastNameError}</span>
          </label>
          <input
            type="text"
            id="ln"
            className="  text-gray-900 text-sm border-b-2 border-black dark:border-gray-600 block w-full p-2.5   dark:placeholder-gray-400 dark:text-white "
            placeholder="family"
            onChange={(e) =>
              setFormData({ ...formData, LastName: e.target.value })
            }
            disabled={logIn.isPending}
            required
          />
          <br />
          <label
            htmlFor="Email"
            className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
          >
            Email <span className="errorLabel">&nbsp;*{errors.emailError}</span>
          </label>
          <input
            type="text"
            id="Email"
            className="  text-gray-900 text-sm border-b-2 border-black dark:border-gray-600 block w-full p-2.5   dark:placeholder-gray-400 dark:text-white "
            placeholder="Email@example.com"
            onChange={(e) =>
              setFormData({ ...formData, Email: e.target.value })
            }
            disabled={logIn.isPending}
            required
          />
          <br />
          <label
            htmlFor="Password"
            className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
          >
            Password{" "}
            <span className="errorLabel">&nbsp;*{errors.passwordError}</span>
          </label>
          <input
            type="password"
            id="Password"
            className="  text-gray-900 text-sm border-b-2 border-black dark:border-gray-600 block w-full p-2.5   dark:placeholder-gray-400 dark:text-white"
            // className="bg-gray-50 border  text-gray-900 text-sm rounded-lg   block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white "
            placeholder="********"
            onChange={(e) =>
              setFormData({ ...formData, Password: e.target.value })
            }
            required
            disabled={logIn.isPending}
          />
          <br /> <br />
          <button
            className="border-2 border-black-600 rounded-lg w-16 h-10 bg-gray-600 text-gray-900 text-sm dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white"
            onClick={() => {
              logIn.mutate();
            }}
            disabled={logIn.isPending}
          >
            {logIn.isPending ? <LoadingSpin /> : <p>Sign up</p>}
          </button>
          <br /> <br />
          <hr
            style={{ borderWidth: "1px" }}
            className="border-black dark:border-gray-600 "
          />{" "}
          <br />
          <p>
            Already have an account? <Link href="/Login">Login</Link>
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
