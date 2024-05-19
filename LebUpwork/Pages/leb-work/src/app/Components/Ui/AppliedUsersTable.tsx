"use client";
import "@/app/SCSS/Table.scss";
import axiosInstance from "@/app/Hooks/axiosInstanse";
import useToast from "@/app/Hooks/useToast";
import { useEffect, useState } from "react";
import useApi from "@/app/Hooks/useApi";
import { differenceInHours, formatDistanceToNow, parseISO } from "date-fns";
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
function JobInfo({ jobId }: { jobId: string }) {
  const [Loading, isLoading] = useState(false);
  const [data, setData] = useState<any>([]);
  const [theSelectedUser, setTheSelectedUserId] = useState("0");
  async function selectUserForJob(userId: string) {
    try {
      const { data } = await axiosInstance.put(`/Job/UpdateJobSelectedUser`, {
        selectedUserId: userId,
        jobId: jobId,
      });
      setChangeCount(changeCount + 1);
      setTheSelectedUserId(userId);
    } catch (err: any) {
      useToast({
        status: "error",
        description: err?.response.data || "something went wrong!",
      });
    }
  }
  async function getJobAppliedUsers() {
    isLoading(true);
    try {
      const response = await axiosInstance.get(
        `/AppliedToTask/GetUsersAppliedByTaskId?JobId=${jobId}`
      );
      console.log(response.data.$values[0].selectedCount);
      setChangeCount(response.data.$values[0].selectedCount);
      setTheSelectedUserId(response.data.$values[0].selectedUserId);

      if (response.data)
        isPostOlderThan24Hours(response.data.$values[0].postedDate);
      setData(response.data.$values);
    } catch (err: any) {
      console.log(err);
      useToast({
        status: "error",
        description: "something went wrong!",
      });
    }
    isLoading(false);
  }
  function isPostOlderThan24Hours(postedDate: any) {
    console.log(changeCount);
    const parsedPostedDate = parseISO(postedDate);
    const currentDate = new Date();
    const hoursDifference = differenceInHours(currentDate, parsedPostedDate);

    setCanChange(!(hoursDifference > 24) && changeCount <= 3);
    console.log(hoursDifference > 24);
  }

  useEffect(() => {
    getJobAppliedUsers();
  }, []);

  const [canChange, setCanChange] = useState(false);
  const [changeCount, setChangeCount] = useState(0);
  return (
    <>
      {!canChange ? (
        <h1>"You can't selet users anymore"</h1>
      ) : (
        <h1>You can change your selection {changeCount} </h1>
      )}
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
                <th>Select User</th>
              </tr>
            </thead>
            <tbody>
              {data.map((data: any, index: any) => (
                <tr key={index}>
                  <td>
                    {data.user.profilePicture ? (
                      <img
                        height="50px"
                        width="50px"
                        src={
                          useApi +
                          "/File/Image?ImageName=" +
                          data.user.profilePicture
                        }
                      ></img>
                    ) : (
                      <img
                        height="50px"
                        width="50px"
                        src="Images/defaultProfilePicture.png"
                      ></img>
                    )}
                  </td>
                  <td>{data.user.firstName}</td>
                  <td>{data.user.lastName}</td>
                  <td>
                    {formatDistanceToNow(parseISO(data.appliedDate), {
                      addSuffix: true,
                    })}
                  </td>
                  <td>
                    {canChange ? (
                      <button
                        style={{ margin: "auto", padding: "20px" }}
                        onClick={() => {
                          if (data.user.userId == theSelectedUser) return;
                          selectUserForJob(data.user.userId);
                        }}
                      >
                        {data.user.userId != theSelectedUser
                          ? "select"
                          : "selected"}
                      </button>
                    ) : (
                      ""
                    )}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
    </>
  );
}

export default JobInfo;
