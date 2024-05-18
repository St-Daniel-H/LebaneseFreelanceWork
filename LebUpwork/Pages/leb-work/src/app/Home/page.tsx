"use client";

import { useEffect, useState } from "react";
import UserProfilePop from "../Components/Ui/UserProfilePop";
import ValidateToken from "../Components/Providers/ValidateToken";
import { useRouter } from "next/navigation";
import JobsTable from "../Components/Ui/JobsTable";
import "@/app/SCSS/Home.scss";
import MyJobsTable from "../Components/Ui/MyJobsTable";
import JobsAppliedTo from "../Components/Ui/JobsAppliedTo";

function getToken() {
  return localStorage.getItem("token") || sessionStorage.getItem("token");
}

const jwt = require("jsonwebtoken");

export default function Home() {
  const rout = useRouter();

  const [userId, setUserId] = useState(0);
  function decodeJwt() {
    try {
      const decoded = jwt.decode(getToken());
      const nameIdentifier =
        decoded[
          "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
        ];
      setUserId(nameIdentifier);
      return nameIdentifier;
    } catch (error) {
      console.error("Error decoding JWT:", error);
      return null;
    }
  }

  useEffect(() => {
    const token =
      localStorage.getItem("token") || sessionStorage.getItem("token");

    if (!token) {
      rout.push("/login");
    }
    decodeJwt();
  }, []);

  const [selectedTab, setSelectedTab] = useState("Jobs");

  function Table(): React.ReactNode {
    switch (selectedTab) {
      case "Jobs":
        return <JobsTable />;
      case "My Jobs":
        return <MyJobsTable />;
      case "Jobs Applied To":
        return <JobsAppliedTo userId={userId} />;

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
