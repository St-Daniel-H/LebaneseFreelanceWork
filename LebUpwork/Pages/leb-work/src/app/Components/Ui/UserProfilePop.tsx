import { useQuery } from "@tanstack/react-query";
import axios from "axios";
import useApi from "@/app/Hooks/useApi";
import useToast from "@/app/Hooks/useToast";
import axiosInstance from "@/app/Hooks/axiosInstanse";
import { useState } from "react";
import { CiUser } from "react-icons/ci";
import "@/app/SCSS/UserInfoPop.scss";
function UserProfilePop() {
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
        <div className="UserNavOption">
          <h1>Jobs</h1>
        </div>
        <div className="UserNavOption">
          <h1>Applied Jobs</h1>
        </div>
      </div>
      <div id="SignoutOption" className="UserNavOption">
        <h1>Sign out</h1>
      </div>
    </div>
  );
}
export default UserProfilePop;
