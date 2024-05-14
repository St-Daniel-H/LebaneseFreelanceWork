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
    <div id="UserInfoPop">
      <div id="UserProfilePop">
        <div>
          {" "}
          {data.profilePicture} <img src=""></img>
        </div>
        <div>
          <h1>
            {data?.firstName} {data?.lastName}
          </h1>
        </div>
      </div>
      <div id="UserOptions"></div>
      <div id="SignoutOption"></div>
    </div>
  );
}
export default UserProfilePop;
