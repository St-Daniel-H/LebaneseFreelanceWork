import { useQuery } from "@tanstack/react-query";
import axios from "axios";
import useApi from "@/app/Hooks/useApi";
import useToast from "@/app/Hooks/useToast";
import axiosInstance from "@/app/Hooks/axiosInstanse";
import { useState } from "react";
import { CiUser } from "react-icons/ci";
import "@/app/SCSS/UserInfoPop.scss";
interface UserProfilePopProps {
  selectedTab: string;
  setSelectedTab: (tab: string) => void;
}
function UserProfilePop({ selectedTab, setSelectedTab }: UserProfilePopProps) {
  const { data, isLoading, isError } = useQuery({
    queryKey: ["myUserInfo"],
    queryFn: async () => {
      const { data } = await axiosInstance.get(`/User/UserInfo`);
      console.log(data);
      return data;
    },
  });

  if (isError) {
    useToast({ status: "error", description: "Something went wrong" });
  }
  return (
    <div id="UserInfoPop" className="shadow">
      <div id="UserProfilePop">
        <div>
          {" "}
          {data && data.profilePicture ? (
            <img
              src={useApi + "/File/Image?ImageName=" + data.profilePicture}
            ></img>
          ) : (
            <img src="Images/defaultProfilePicture.png"></img>
          )}
        </div>
        <div style={{ marginLeft: "10px" }}>
          <h1>
            {data?.firstName} {data?.lastName}
          </h1>
          <b>Tokens:</b> <i>{data?.token}</i>
        </div>
      </div>
      <div id="UserOptions">
        <div
          onClick={() => {
            setSelectedTab("Jobs");
          }}
          className={` UserNavOption ${
            selectedTab == "Jobs" ? " selectedNavOption" : ""
          }`}
        >
          <h1>Jobs</h1>
        </div>
        <div
          onClick={() => {
            setSelectedTab("My Jobs");
          }}
          className={` UserNavOption ${
            selectedTab == "My Jobs" ? " selectedNavOption" : ""
          }`}
        >
          <h1>My Jobs</h1>
        </div>
        <div
          onClick={() => {
            setSelectedTab("Jobs Applied To");
          }}
          className={` UserNavOption ${
            selectedTab == "Jobs Applied To" ? " selectedNavOption" : ""
          }`}
        >
          <h1>Jobs Applied To</h1>
        </div>
        <div id="SignoutOption" className="UserNavOption">
          <h1>Sign out</h1>
        </div>
      </div>
    </div>
  );
}
export default UserProfilePop;
