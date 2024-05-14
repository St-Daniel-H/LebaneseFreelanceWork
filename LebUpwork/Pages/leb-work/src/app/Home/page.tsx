"use client";

import { useEffect } from "react";
import TopBar from "../Components/Ui/TopBar";
import UserProfilePop from "../Components/Ui/UserProfilePop";
import ValidateToken from "../Components/Providers/ValidateToken";
import { useRouter } from "next/navigation";
import "@/app/SCSS/Home.scss";
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
  return (
    <div id="Home">
      <div id="HomeBody">
        <UserProfilePop />
      </div>
    </div>
  );
}
