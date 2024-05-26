"use client";
import { FaPlus } from "react-icons/fa";
import "@/app/SCSS/AddNewJobPopup.scss";
import { useState } from "react";
import axiosInstance from "@/app/Hooks/axiosInstanse";
import useToast from "@/app/Hooks/useToast";
import { IoIosCloseCircle } from "react-icons/io";
import { useRouter } from "next/navigation";
export default function AddNewJobPopup() {
  const [open, setOpen] = useState<any>(false);
  const handleClose = () => {
    setOpen(false);
  };
  return (
    <>
      <div
        id="AddNewJobPopup"
        onClick={() => {
          setOpen(!open);
        }}
      >
        <FaPlus />
      </div>
      {open && <NewJobPopupForm open={open} handleClose={handleClose} />}
    </>
  );
}
function NewJobPopupForm({
  open,
  handleClose,
}: {
  open: any;
  handleClose: () => any;
}) {
  const [errors, setErrors] = useState({
    titleError: "",
    descriptionError: "",
    offerError: "",
    tagsError: "",
  });
  const [formData, setFormData] = useState<any>({
    title: "",
    description: "",
    offer: 0,
    tags: [],
  });
  const [isPending, setIsPending] = useState(false);
  const [options, setOptions] = useState<{ tagId: string; tagName: string }[]>(
    []
  );
  const [filter, setFilter] = useState("");

  async function getTagsByName(filter: string) {
    if (filter == "") {
      setOptions([]);
      return;
    }
    const data = await axiosInstance.get("/Tag/GetTagsByName?name=" + filter);
    console.log(data.data.$values);
    const tags = data.data.$values.map(
      (tag: { tagId: string; tagName: string }) => ({
        tagId: tag.tagId,
        tagName: tag.tagName,
      })
    );
    setOptions(tags);
    return data;
  }
  async function CreateJobTag() {
    try {
      const response = await axiosInstance.post("/Tag/CreateTag", {
        tagName: filter,
      });
      useToast({ status: "success", description: "Job tag created" });
      console.log(response.data);
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
  const [selectedTags, setSelectedTags] = useState<any>([]);
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

  async function CreateNewJob() {
    try {
      const response = await axiosInstance.post("/Job/PostJob", formData);
      console.log(response);
      window.location.reload();
    } catch (err) {
      console.log(err);
      useToast({ status: "error", description: "Something went wrong" });
    }
  }
  //const options = ["Option 1", "Option 2", "Option 3", "Option 4", "Option 5"];
  return open ? (
    <div id="NewJobPopupFormContainer">
      <div id="NewJobPopupForm">
        <div id="ModalTopJobForm">
          {" "}
          <h1>Post a new Job Ad!</h1>
          <IoIosCloseCircle
            style={{ fontSize: "25px", cursor: "pointer" }}
            onClick={() => {
              handleClose();
            }}
          />
        </div>
        <div id="MiddleJobForm">
          {" "}
          <label
            htmlFor="title"
            className="block text-sm font-medium text-gray-900 dark:text-white"
          >
            Job Title{" "}
            <span className="errorLabel">&nbsp;*{errors.titleError}</span>
          </label>
          <input
            value={formData.title}
            id="title"
            className="  text-gray-900 text-sm border-b-2 border-black dark:border-gray-600 block w-full p-2.5   dark:placeholder-gray-400 dark:text-white"
            // className="bg-gray-50 border  text-gray-900 text-sm rounded-lg   block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white "
            placeholder="Title"
            onChange={(e) =>
              setFormData({ ...formData, title: e.target.value })
            }
            required
            disabled={isPending}
          />
          <br />{" "}
          <label
            htmlFor="description"
            className="block  text-sm font-medium text-gray-900 dark:text-white"
          >
            Description{" "}
            <span className="errorLabel">&nbsp;*{errors.descriptionError}</span>
          </label>
          <textarea
            id="description"
            value={formData.description}
            className="  text-gray-900 text-sm border-b-2 border-black dark:border-gray-600 block w-full p-2.5   dark:placeholder-gray-400 dark:text-white"
            // className="bg-gray-50 border  text-gray-900 text-sm rounded-lg   block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white "
            placeholder="Description"
            onChange={(e) =>
              setFormData({ ...formData, description: e.target.value })
            }
            required
            disabled={isPending}
          />
          <br />
          <label
            htmlFor="offer"
            className="block  text-sm font-medium text-gray-900 dark:text-white"
          >
            Offer <span className="errorLabel">&nbsp;*{errors.offerError}</span>
          </label>
          <input
            type="number"
            id="offer"
            className="  text-gray-900 text-sm border-b-2 border-black dark:border-gray-600 block w-full p-2.5   dark:placeholder-gray-400 dark:text-white"
            // className="bg-gray-50 border  text-gray-900 text-sm rounded-lg   block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white "
            placeholder="Title"
            value={formData.offer}
            onChange={(e) => {
              if (parseInt(e.target.value) >= 0) {
                setFormData({ ...formData, offer: e.target.value });
              } else {
                setFormData({ ...formData, offer: 0 });
              }
            }}
            required
            disabled={isPending}
          />
          <br />
          <div>
            <br />
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
                className="border border-gray-300 bg-white rounded-md shadow-sm"
              >
                {options.map((option: any) => (
                  <div
                    key={option.tagId}
                    style={{
                      color: "white",
                      backgroundColor: formData.tags.some(
                        (tag: any) => tag.tagId === option.tagId
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
                <span id="selected-label" className="font-medium text-gray-900">
                  {selectedTags.length ? selectedTags.join(", ") : "None"}
                </span>
              </p>
            </div>
          </div>
          <div id="PostJobBottom">
            <button
              onClick={() => {
                console.log(formData);
                CreateNewJob();
              }}
            >
              Post
            </button>
          </div>
        </div>
      </div>
    </div>
  ) : (
    ""
  );
}
