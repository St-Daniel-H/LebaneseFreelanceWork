import { useState } from "react";
import LoadingSpin from "./LoadingSpin";
import { formatDistanceToNow, parseISO } from "date-fns";
import useApi from "@/app/Hooks/useApi";
import axiosInstance from "@/app/Hooks/axiosInstanse";
import useToast from "@/app/Hooks/useToast";
export default function JobsTable({
  isLoading,
  data,
  skip,
  setSkip,
  GetMoreJobs,
  isGettingMoreJobs,
  noMoreData,
  isKeywordLoading,
  keyword,
  getJobsWithKeyword,
}: {
  keyword: any;
  getJobsWithKeyword: any;
  isKeywordLoading: boolean;
  isLoading: boolean;
  data: any;
  skip: any;
  setSkip: any;
  GetMoreJobs: any;
  isGettingMoreJobs: any;
  noMoreData: boolean;
}) {
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
    <div id="JobsContainer">
      <div id="JobsTable">
        {!isLoading && data && !isKeywordLoading ? (
          <div id="JobsGrid">
            <div id="Jobs">
              {data.map((job: any, index: any) => (
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
                  setSkip(skip + 10);
                  if (keyword == "") {
                    GetMoreJobs();
                  } else getJobsWithKeyword();
                }}
                id="loadMoreBtn"
                className="job-item"
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
            <div id="JobDescription">
              {selectedJob.title != "" ? (
                <>
                  <div>
                    {" "}
                    <div id="job-title-field" style={{ padding: "10px" }}>
                      <h1>{selectedJob.title}</h1>
                    </div>
                    <div id="job-description-field" style={{ padding: "10px" }}>
                      {" "}
                      <p>{selectedJob.description}</p>
                    </div>
                    <div id="bottom">
                      <div id="job-tags" style={{ padding: "10px" }}>
                        <h1>Tags:</h1>

                        {selectedJob.tags.map((tag: any, index: any) => (
                          <div className="tag-item" key={index}>
                            {tag.tagName}
                          </div>
                        ))}
                      </div>

                      <div id="job-description-bottom">
                        <div id="job-offer">{selectedJob.offer}$</div>
                        <div>
                          {isApplying ? (
                            <LoadingSpin />
                          ) : (
                            <button
                              id="applyToJobBtn"
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
  );
}
