"use client";
import "@/app/SCSS/Table.scss";
import axiosInstance from "@/app/Hooks/axiosInstanse";
import useToast from "@/app/Hooks/useToast";
import { useEffect, useState } from "react";
import useApi from "@/app/Hooks/useApi";
// {
//     "appliedDate": "2024-05-18T18:04:24.6279006",
//     "jobId": 28,
//     "userId": 9,
//     "user": {
//         "$id": "3",
//         "userId": 9,
//         "firstName": "ss",
//         "lastName": "ssss",
//         "isOnline": null,
//         "profilePicture": null
//     }
// },
function JobInfo({ params }: { params: any }) {
  const jobId = params.JobId;
  const [Loading, isLoading] = useState(false);
  const [data, setData] = useState<any>([]);
  async function getJobAppliedUsers() {
    isLoading(true);
    try {
      const { data } = await axiosInstance.get(
        `/AppliedToTask/GetUsersAppliedByTaskId?JobId=${jobId}`
      );
      setData(data.$values);
    } catch (err: any) {
      useToast({
        status: "error",
        description: err?.response.data || "something went wrong!",
      });
    }
    isLoading(false);
  }
  useEffect(() => {
    getJobAppliedUsers();
  }, []);
  return (
    <div id="JobInfo">
      {Loading ? (
        "Loading..."
      ) : (
        <table>
          <thead>
            <tr>
              <th>Profile Picture</th>
              <th>First Name</th>
              <th>Last Name</th>
              <th>Applied Date</th>
            </tr>
          </thead>
          <tbody>
            {data.map((data: any, index: any) => (
              <tr key={index}>
                <td>
                  <img
                    src={
                      useApi +
                      "/File/Image?ImageName=" +
                      data.user.profilePicture
                    }
                    alt={`${data.user.firstName} ${data.user.lastName}`}
                    width="50"
                    height="50"
                  />
                </td>
                <td>{data.user.firstName}</td>
                <td>{data.user.lastName}</td>
                <td>{data.appliedDate}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
}

export default JobInfo;
