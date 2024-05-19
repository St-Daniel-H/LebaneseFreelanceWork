import axiosInstance from "@/app/Hooks/axiosInstanse";
import useToast from "@/app/Hooks/useToast";
import { useEffect, useRef, useState } from "react";
import { useQuery } from "@tanstack/react-query";
import LoadingSpin from "./LoadingSpin";
import useApi from "@/app/Hooks/useApi";
import { formatDistanceToNow, parseISO } from "date-fns";

import "@/app/SCSS/Profile.scss";
import Link from "next/link";
import { useRouter } from "next/navigation";
import useSWR from "swr";
import { FaPen } from "react-icons/fa";
import JobsFinishedByUserId from "./JobsFinishedByUserId";
function MyProfile({ userId, myOwn }: { userId: string; myOwn: boolean }) {
  const rout = useRouter();
  const [formData, setFormData] = useState<any>({});
  if (myOwn) {
    const { data, isLoading, isError } = useQuery({
      queryKey: ["myUserInfo"],
      queryFn: async () => {
        const { data } = await axiosInstance.get(`/User/UserInfo`);
        console.log(data);
        setFormData(data);
        return data;
      },
    });
  }

  const formattedDateDistance = (date: string) => {
    if (date)
      return formatDistanceToNow(parseISO(date), {
        addSuffix: true,
      });
    else return "";
  };

  const fileInputRef = useRef(null);
  const [visible, setVisible] = useState(false);
  return formData ? (
    <div className="Profile">
      <div className="TopProfile">
        <div>
          {" "}
          {formData && formData.profilePicture ? (
            <img
              height="200px"
              width="200px"
              style={{ borderRadius: "50%" }}
              src={useApi + "/File/Image?ImageName=" + formData.profilePicture}
            ></img>
          ) : (
            <img src="Images/defaultProfilePicture.png"></img>
          )}
          <label
            className="fileInputLabel"
            htmlFor="pfpFileInput"
            style={{ cursor: "pointer", width: "20%", height: "40px" }}
          >
            <FaPen />
          </label>{" "}
          <input
            id="pfpFileInput"
            type="file"
            ref={fileInputRef}
            style={{}}
            onChange={(ev) =>
              setFormData({ ...formData, profilePicture: ev.target.files })
            }
          />
        </div>
        <div className="ProfileTopInfo">
          <label htmlFor="styled-multiselect">Name</label>
          <input
            id="profileName"
            className="  text-gray-900 text-sm border-b-2 border-black dark:border-gray-600 block w-full p-2.5   dark:placeholder-gray-400 dark:text-white"
            // className="bg-gray-50 border  text-gray-900 text-sm rounded-lg   block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white "
            placeholder="Search for tag"
            value={formData.firstName + " " + formData.lastName}
            disabled
          />
          <br />
          <label htmlFor="styled-multiselect">Joined Date</label>
          <input
            id="profileJoinedDate"
            className="  text-gray-900 text-sm border-b-2 border-black dark:border-gray-600 block w-full p-2.5   dark:placeholder-gray-400 dark:text-white"
            // className="bg-gray-50 border  text-gray-900 text-sm rounded-lg   block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white "
            placeholder="Search for tag"
            value={formData ? formattedDateDistance(formData.joinedDate) : ""}
            disabled
          />
          <br />
        </div>
      </div>
      <div className="BottomProfile">
        <div className="BottomLeftProfile">
          <br />
          <label htmlFor="styled-multiselect">Phone Number</label>
          <input
            id="profilePhoneNumber"
            className="  text-gray-900 text-sm border-b-2 border-black dark:border-gray-600 block w-full p-2.5   dark:placeholder-gray-400 dark:text-white"
            placeholder="Phone Number"
            value={formData.phoneNumber || ""}
            onChange={(e) => {
              setFormData({ ...formData, phoneNumber: e.target.value });
            }}
          />
          <br />
          <textarea
            id="status"
            value={formData.description}
            className="  text-gray-900 text-sm border-b-2 border-black dark:border-gray-600 block w-full p-2.5   dark:placeholder-gray-400 dark:text-white"
            placeholder="Status"
            onChange={(e) =>
              setFormData({ ...formData, status: e.target.value })
            }
            required
          />
          <br />
          <button style={{ padding: "10px" }}>Save Changes</button>
        </div>
        <div className="BottomRightProfile">
          <br />
          <label htmlFor="styled-multiselect">CV</label>
          <div
            style={{
              display: "flex",
              alignItems: "center",
              justifyContent: "center",
            }}
          >
            {formData.cVpdf ? (
              <>
                <button
                  className="  text-gray-900 text-sm border-b-2 border-black dark:border-gray-600 block w-full p-2.5   dark:placeholder-gray-400 dark:text-white disabled"
                  style={{ cursor: "pointer", width: "80%" }}
                  onClick={() => {
                    console.log("hi");
                    const url = `${useApi}/File/Pdf?pdfName=${formData.cVpdf}`;
                    window.open(url, "_blank");
                  }}
                >
                  {formData.firstName}'s CV
                </button>
                <label
                  className="fileInputLabel"
                  htmlFor="cvFileInput"
                  style={{ cursor: "pointer", width: "20%", height: "40px" }}
                >
                  <FaPen />
                </label>{" "}
                <input
                  id="cvFileInput"
                  type="file"
                  ref={fileInputRef}
                  style={{}}
                  onChange={(ev) =>
                    setFormData({ ...formData, cVpdf: ev.target.files })
                  }
                />
              </>
            ) : (
              ""
            )}
            <br />

            {/* <button
              className="  text-gray-900 text-sm border-b-2 border-black dark:border-gray-600 block w-full p-2.5   dark:placeholder-gray-400 dark:text-white disabled"
              style={{ cursor: "pointer", width: "20%" }}
              onClick={() => {
                fileInputRef.current.click();
              }}
            > */}
            {/* Change CV
            </button> */}
          </div>{" "}
          <br />
          <button
            className="  text-gray-900 text-sm border-b-2 border-black dark:border-gray-600 block w-full p-2.5   dark:placeholder-gray-400 dark:text-white disabled"
            style={{ cursor: "pointer", width: "100%" }}
            onClick={() => {
              setVisible(true);
            }}
          >
            View Previous Work
          </button>
          <br />
          <div id="MyTags"></div>
        </div>
      </div>
      <JobsFinishedByUserId
        userId={userId}
        isVisible={visible}
        setIsVisible={setVisible}
      />
    </div>
  ) : (
    <div>
      <LoadingSpin />
    </div>
  );
}
export default MyProfile;
