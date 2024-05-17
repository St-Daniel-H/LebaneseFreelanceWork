"use client";

import { useEffect, useState } from "react";
import TopBar from "../Components/Ui/TopBar";
import UserProfilePop from "../Components/Ui/UserProfilePop";
import ValidateToken from "../Components/Providers/ValidateToken";
import { useRouter } from "next/navigation";
import JobsTable from "../Components/Ui/JobsTable";
import "@/app/SCSS/Home.scss";
import axiosInstance from "../Hooks/axiosInstanse";
import { useQuery } from "@tanstack/react-query";
const jwt = require("jsonwebtoken");

export default function Home() {
  const rout = useRouter();

  useEffect(() => {
    const token =
      localStorage.getItem("token") || sessionStorage.getItem("token");

    if (!token) {
      rout.push("/login");
    }
  }, []);
  useEffect(() => {
    ValidateToken();
  }, []);
  const [jobs, setJobs] = useState<any[]>([]);
  const [skip, setSkip] = useState(0);
  const { data, isLoading, isError } = useQuery({
    queryKey: ["GetJobsByUserTags"],
    queryFn: async () => {
      const { data } =
        await axiosInstance.get(`/Job/GetJobsWithSimilarTag?skip=${skip}&pageSize=10
      `);
      setJobs(data.$values);
      return data;
    },
  });
  const [isGettingMoreJobs, setIsGettingMoreJobs] = useState(false);
  const [noMoreData, setNoMoreData] = useState(false);
  async function GetMoreJobs() {
    try {
      setIsGettingMoreJobs(true);
      const response = await axiosInstance.get(
        `/Job/GetJobsWithSimilarTag?skip=${skip}&pageSize=10`
      );
      const newData = response.data;
      if (newData.$values.length == 0) setNoMoreData(true);
      setJobs(jobs.concat(newData.$values));
      setIsGettingMoreJobs(false);
      return newData; // Return the new data if needed
    } catch (error) {
      console.error("Error fetching more jobs:", error);
      setIsGettingMoreJobs(false);
      // Handle errors here
    }
  }

  const [keyword, setKeyword] = useState("");
  const [isKeywordLoading, setIsKeywordLoading] = useState(false);
  async function getJobsWithKeyword() {
    setNoMoreData(false);

    try {
      if (keyword == "") {
        const { data } =
          await axiosInstance.get(`/Job/GetJobsWithSimilarTag?skip=${skip}&pageSize=10
      `);
        setJobs(data.$values);
        return;
      }
      setIsKeywordLoading(true);
      setIsGettingMoreJobs(true);
      const response = await axiosInstance.get(
        `/Job/GetJobsWithKeywords?skip=${skip}&pageSize=10&keyword=${keyword}`
      );
      const newData = response.data;
      if (newData.$values.length == 0) setNoMoreData(true);
      if (skip == 0) setJobs(newData.$values);
      else setJobs(jobs.concat(newData.$values));
      setIsGettingMoreJobs(false);
      setIsKeywordLoading(false);
      return newData; // Return the new data if needed
    } catch (error) {
      console.error("Error fetching more jobs:", error);
      setIsGettingMoreJobs(false);
      setIsKeywordLoading(false);
      // Handle errors here
    }
  }
  return (
    <div id="Home">
      <div id="HomeBody">
        <UserProfilePop />
        <div
          style={{
            height: "100%",
            width: "100%",
          }}
        >
          <div
            style={{
              display: "flex",
              alignItems: "center",
              justifyContent: "center",
              marginTop: "20px",
            }}
          >
            {" "}
            <div
              id="search-bar"
              style={{
                borderTopLeftRadius: "10px",
                borderBottomLeftRadius: "10px",
              }}
            >
              <input
                placeholder="Search"
                onChange={(e) => {
                  setKeyword(e.target.value);
                }}
              ></input>
              <button
                onClick={() => {
                  setSkip(0);
                  getJobsWithKeyword();
                }}
              >
                Search
              </button>
            </div>
          </div>
          <JobsTable
            skip={skip}
            setSkip={setSkip}
            isLoading={isLoading}
            data={jobs}
            GetMoreJobs={GetMoreJobs}
            noMoreData={noMoreData}
            isGettingMoreJobs={isGettingMoreJobs}
            isKeywordLoading={isKeywordLoading}
            keyword={keyword}
            getJobsWithKeyword={getJobsWithKeyword}
          />
        </div>
      </div>
    </div>
  );
}
