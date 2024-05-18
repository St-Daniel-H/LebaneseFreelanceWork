"use client";

import { ReactNode, useEffect, useState } from "react";
import UserProfilePop from "../Components/Ui/UserProfilePop";
import ValidateToken from "../Components/Providers/ValidateToken";
import { useRouter } from "next/navigation";
import JobsTable from "../Components/Ui/JobsTable";
import "@/app/SCSS/Home.scss";
import axiosInstance from "../Hooks/axiosInstanse";
import { useQuery } from "@tanstack/react-query";
import MyJobsTable from "../Components/Ui/MyJobsTable";

export default function Home() {
  const rout = useRouter();

  useEffect(() => {
    const token =
      localStorage.getItem("token") || sessionStorage.getItem("token");

    if (!token) {
      rout.push("/login");
    }
  }, []);
  useEffect(() => {
    ValidateToken();
  }, []);

  const [selectedTab, setSelectedTab] = useState("Jobs");

  function Table(): React.ReactNode {
    switch (selectedTab) {
      case "Jobs":
        return <JobsTable />;
      case "My Jobs":
        return <MyJobsTable />;
      default:
        return <></>;
    }
  }
  return (
    <div id="Home">
      <div id="HomeBody">
        <UserProfilePop
          selectedTab={selectedTab}
          setSelectedTab={setSelectedTab}
        />{" "}
        {Table()}
      </div>
    </div>
  );
}
