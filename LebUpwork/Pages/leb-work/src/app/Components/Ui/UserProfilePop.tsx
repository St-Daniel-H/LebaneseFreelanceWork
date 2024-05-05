import { useQuery } from "@tanstack/react-query";
import axios from "axios";
import useApi from "@/app/Hooks/useApi";
import useToast from "@/app/Hooks/useToast";
import axiosInstance from "@/app/Hooks/axiosInstanse";
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
    <div id="UserProfilePop">
      {/* <div id="UserInfo">{JSON.parse(data)}</div> */}
    </div>
  );
}
export default UserProfilePop;
