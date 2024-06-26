import { useState } from "react";
import LoadingSpin from "./LoadingSpin";
import { formatDistanceToNow, parseISO } from "date-fns";
import useApi from "@/app/Hooks/useApi";
import axiosInstance from "@/app/Hooks/axiosInstanse";
import useToast from "@/app/Hooks/useToast";
import { useQuery } from "@tanstack/react-query";
export default function JobsTable({}: // isLoading,
// data,
// skip,
// setSkip,
// GetMoreJobs,
// isGettingMoreJobs,
// noMoreData,
// isKeywordLoading,
// keyword,
// getJobsWithKeyword,
{
  // keyword: any;
  // getJobsWithKeyword: any;
  // isKeywordLoading: boolean;
  // isLoading: boolean;
  // data: any;
  // skip: any;
  // setSkip: any;
  // GetMoreJobs: any;
  // isGettingMoreJobs: any;
  // noMoreData: boolean;
}) {
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
  async function GetMoreJobs(skip: any) {
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
  const [selectedJob, setSelectedJob] = useState({
    jobId: "",
    title: "",
    description: "",
    offer: 0,
    postedDate: "",
    tags: [],
  });
  const [isApplying, setIsApplying] = useState(false);
  async function ApplyToJob() {
    setIsApplying(true);
    try {
      const response = await axiosInstance.get(
        `/AppliedToTask/ApplyToJob?jobId=${selectedJob.jobId}`
      );
      const newData = response.data;
      setIsApplying(false);
      useToast({
        status: "success",
        description: "Applied to job",
      });
      return newData;
    } catch (error: any) {
      setIsApplying(false);
      console.error("Error fetching more jobs:", error?.response?.data);
      useToast({
        status: "error",
        description: error?.response?.data,
      });
    }
  }
  return (
    <div className="JobsContainer" style={{ display: "block" }}>
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
      <div
        style={{
          width: "100%",
          display: "flex",
          alignItems: "center",
          justifyContent: "center",
          marginTop: "50px",
        }}
      >
        <div className="JobsTable">
          {!isLoading && jobs && !isKeywordLoading ? (
            <div className="JobsGrid">
              <div className="Jobs">
                {jobs.map((job: any, index: any) => (
                  <div
                    key={index}
                    className={`job-item ${
                      selectedJob.jobId === job.jobId ? "selected" : ""
                    }`}
                    onClick={() =>
                      setSelectedJob({
                        jobId: job.jobId,
                        title: job.title,
                        description: job.description,
                        offer: job.offer,
                        postedDate: job.postedDate,
                        tags: job.tags.$values,
                      })
                    }
                  >
                    <h3>{job.title}</h3>
                    <div style={{ display: "flex" }}>
                      <img
                        style={{
                          borderRadius: "50%",
                          height: "40px",
                          width: "40px",
                        }}
                        src={
                          useApi +
                          "/File/Image?ImageName=" +
                          job.user.profilePicture
                        }
                      ></img>
                      <div style={{ marginLeft: "10px" }}>
                        <p style={{ fontSize: "13px" }}>
                          {job.user.firstName} {job.user.lastName}
                        </p>{" "}
                        <i style={{ fontSize: "13px" }}>
                          {formatDistanceToNow(parseISO(job.postedDate), {
                            addSuffix: true,
                          })}
                        </i>
                      </div>
                    </div>
                  </div>
                ))}
                <div
                  onClick={() => {
                    if (keyword == "") {
                      GetMoreJobs(skip + 10);
                      setSkip(skip + 10);
                    } else getJobsWithKeyword();
                  }}
                  className="loadMoreBtn job-item"
                  style={{ borderBottom: "0px" }}
                >
                  {noMoreData ? (
                    ""
                  ) : (
                    <>
                      {!isGettingMoreJobs ? (
                        <h1>Load More</h1>
                      ) : (
                        <div style={{ maxWidth: "100%", maxHeight: "100%" }}>
                          <LoadingSpin />
                        </div>
                      )}
                    </>
                  )}
                </div>
              </div>
              <div className="JobDescription">
                {selectedJob.title != "" ? (
                  <>
                    <div>
                      {" "}
                      <div
                        className="job-title-field"
                        style={{ padding: "10px" }}
                      >
                        <h1>{selectedJob.title}</h1>
                      </div>
                      <div
                        className="job-description-field"
                        style={{ padding: "10px" }}
                      >
                        {" "}
                        <p>{selectedJob.description}</p>
                      </div>
                      <div className="bottom">
                        <div className="job-tags" style={{ padding: "10px" }}>
                          <h1>Tags:</h1>

                          {selectedJob.tags.map((tag: any, index: any) => (
                            <div className="tag-item" key={index}>
                              {tag.tagName}
                            </div>
                          ))}
                        </div>

                        <div className="job-description-bottom">
                          <div className="job-offer">{selectedJob.offer}$</div>
                          <div>
                            {isApplying ? (
                              <LoadingSpin />
                            ) : (
                              <button
                                className="applyToJobBtn"
                                onClick={() => {
                                  ApplyToJob();
                                }}
                              >
                                {" "}
                                Apply
                              </button>
                            )}
                          </div>
                        </div>
                      </div>
                    </div>
                  </>
                ) : (
                  ""
                )}
              </div>
            </div>
          ) : (
            <div>
              <LoadingSpin />
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
