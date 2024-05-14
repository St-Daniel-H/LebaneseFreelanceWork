"use client";
import { useEffect } from "react";
import ValidateToken from "./Components/Providers/ValidateToken";
import { useRouter } from "next/navigation";
export default function Home() {
  const rout = useRouter();

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (!token) {
      console.log("go to login fucker");
      rout.push("/login");
    }
  }, []);
  useEffect(() => {
    ValidateToken();
  }, []);
  return <h1>NotHome</h1>;
}
