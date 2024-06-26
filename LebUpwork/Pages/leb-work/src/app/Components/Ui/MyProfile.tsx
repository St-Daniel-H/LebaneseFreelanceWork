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
        setFormData({
          firstName: data.firstName,
          lastName: data.lastName,
          phoneNumber: data.phoneNumber,
          joinedDate: data.joinedDate,
          profilePicture: data.profilePicture,
          status: data.status,
          cVpdf: data.cVpdf,
          tags: [...data.tags.$values],
        });
        setSelectedTags(data.tags.$values.map((x: any) => x.tagName));
        return data;
      },
    });
  }

  const [filter, setFilter] = useState("");
  const [options, setOptions] = useState<{ tagId: string; tagName: string }[]>(
    []
  );
  const handleOptionClick = (option: any) => {
    if (formData.tags.some((tag: any) => tag.tagId == option.tagId)) {
      setFormData({
        ...formData,
        tags: formData.tags.filter((item: any) => item.tagId !== option.tagId),
      });
      setSelectedTags([
        ...selectedTags.filter((item: any) => item !== option.tagName),
      ]);
    } else {
      if (formData.tags.length < 5) {
        setFormData({
          ...formData,
          tags: [...formData.tags, { tagId: option.tagId }],
        });
        setSelectedTags([...selectedTags, option.tagName]);
      }
    }
  };
  const [selectedTags, setSelectedTags] = useState<any>([]);

  async function getTagsByName(filter: string) {
    if (filter == "") {
      setOptions([]);
      return;
    }
    const data = await axiosInstance.get("/Tag/GetTagsByName?name=" + filter);
    const tags = data.data.$values.map(
      (tag: { tagId: string; tagName: string }) => ({
        tagId: tag.tagId,
        tagName: tag.tagName,
      })
    );
    setOptions(tags);
    return data;
  }

  const [newPfp, setNewpfp] = useState<FileList | null>(null);
  const [newCv, setNewCv] = useState<FileList | null>(null);
  async function SaveChanges() {
    console.log(formData.profilePicture);

    if (newCv && newCv.length > 0) {
      const formDataToSend = new FormData();
      formDataToSend.append("cVpdf", newCv[0]);
      const cv = await axiosInstance.put(`User/UpdateCV`, formDataToSend, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      });
    }
    if (newPfp && newPfp.length > 0) {
      const formDataToSend = new FormData();
      formDataToSend.append("profilePicture", newPfp[0]);
      const profilePicture = await axiosInstance.put(
        `User/UpdatePfp`,
        formDataToSend,
        {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        }
      );
    }
    if (formData.tags != null) {
      const transformedTags = formData.tags.map((tag: any) => ({
        tagId: tag.tagId,
      }));
      const tags = await axiosInstance.put(`User/UpdateTags`, {
        tags: transformedTags,
      });
      console.log(tags);
    }
    if (formData.status != null) {
      const status = await axiosInstance.put(`User/UpdateStatus`, {
        status: formData.status,
      });
    }
  }

  async function CreateJobTag() {
    try {
      const response = await axiosInstance.post("/Tag/CreateTag", {
        tagName: filter,
      });
      useToast({ status: "success", description: "Job tag created" });
      setSelectedTags([...selectedTags, response.data.tagName]);
      setFormData({
        ...formData,
        tags: [...formData.tags, { tagId: response.data.tagId }],
      });
    } catch (err) {
      useToast({ status: "error", description: "something went wrong" });
      console.log(err);
    }
    setFilter("");
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
            onChange={(ev) => setNewpfp(ev.target.files)}
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
            value={formData.status}
            className="  text-gray-900 text-sm border-b-2 border-black dark:border-gray-600 block w-full p-2.5   dark:placeholder-gray-400 dark:text-white"
            placeholder="Status"
            onChange={(e) =>
              setFormData({ ...formData, status: e.target.value })
            }
            required
          />
          <br />
          <button style={{ padding: "10px" }} onClick={SaveChanges}>
            Save Changes
          </button>
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
                  onChange={(ev) => setNewCv(ev.target.files)}
                />
              </>
            ) : (
              ""
            )}
            <br />
          </div>{" "}
          <button
            className="  text-gray-900 text-sm border-b-2 border-black dark:border-gray-600 block w-full p-2.5   dark:placeholder-gray-400 dark:text-white disabled"
            style={{ cursor: "pointer", width: "100%" }}
            onClick={() => {
              setVisible(true);
            }}
          >
            View Previous Work
          </button>
          <div id="MyTags">
            <div>
              <label htmlFor="styled-multiselect">Job Skills (Max 5)</label>
              <input
                id="filter"
                className="  text-gray-900 text-sm border-b-2 border-black dark:border-gray-600 block w-full p-2.5   dark:placeholder-gray-400 dark:text-white"
                // className="bg-gray-50 border  text-gray-900 text-sm rounded-lg   block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white "
                placeholder="Search for tag"
                value={filter}
                onChange={(e) => {
                  setFilter(e.target.value);
                  getTagsByName(e.target.value);
                }}
              />
              {options.length == 0 && filter != "" ? (
                <button onClick={CreateJobTag} className="mt-5">
                  Create Job Tag
                </button>
              ) : (
                <></>
              )}
              <div className="relative">
                <div
                  id="selectTagsContainer"
                  style={{ maxHeight: "100px", overflowY: "scroll" }}
                  className="border border-gray-300 bg-white rounded-md shadow-sm"
                >
                  {options.map((option: any) => (
                    <div
                      key={option.tagId}
                      style={{
                        color: "white",
                        backgroundColor: formData.tags.some(
                          (tag: any) => tag.tagId == option.tagId
                        )
                          ? "rgb(205, 14, 4)"
                          : "transparent",
                      }}
                      className={`cursor-pointer select-none py-2 px-4 text-sm `}
                      onClick={() => {
                        handleOptionClick(option);
                      }}
                    >
                      {option.tagName}
                    </div>
                  ))}
                </div>
              </div>
              <div
                id="selected-options"
                className="mt-3 p-2 border border-gray-300 rounded-md bg-white"
              >
                <p className="text-sm text-gray-700">
                  Selected:{" "}
                  <span
                    id="selected-label"
                    className="font-medium text-gray-900"
                  >
                    {selectedTags.length ? selectedTags.join(", ") : "None"}
                  </span>
                </p>
              </div>
            </div>
          </div>
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
