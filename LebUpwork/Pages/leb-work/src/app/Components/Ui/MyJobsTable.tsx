import { useEffect, useState } from "react";
import LoadingSpin from "./LoadingSpin";
import { formatDistanceToNow, parseISO } from "date-fns";
import useApi from "@/app/Hooks/useApi";
import axiosInstance from "@/app/Hooks/axiosInstanse";
import useToast from "@/app/Hooks/useToast";
import AppliedUsersTable from "./AppliedUsersTable";

function getToken() {
  return localStorage.getItem("token") || sessionStorage.getItem("token");
}

const jwt = require("jsonwebtoken");
var userId = 0;

function decodeJwt() {
  try {
    const decoded = jwt.decode(getToken());
    const nameIdentifier =
      decoded[
        "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
      ];
    userId = nameIdentifier;
    return nameIdentifier;
  } catch (error) {
    console.error("Error decoding JWT:", error);
    return null;
  }
}

export default function JobsTable({}: {}) {
  const [isLoading, setIsLoading] = useState(false);
  const [data, setData] = useState<any[]>([]);
  const [noMoreData, setNoMoreData] = useState(false);
  const [skip, setSkip] = useState(0);
  const [selectedJob, setSelectedJob] = useState({
    jobId: "",
    title: "",
    description: "",
    offer: 0,
    postedDate: "",
    tags: [],
  });
  async function GetMyPostedJobs(skip: any) {
    setIsLoading(true);
    try {
      const response = await axiosInstance.get(
        `/Job/ViewJobsPostedByUserId?getUserId=${userId}&skip=${skip}&page=10`
      );
      if (response.data.$values.length == 0) setNoMoreData(true);
      else {
        console.log([...data, ...response.data.$values]);
        if (data.length == 0) {
          setData(response.data.$values);
        } else setData([...data, ...response.data.$values]);
      }
    } catch (err) {
      console.log(err);
      useToast({ status: "error", description: "Something went wrong." });
    }
    setIsLoading(false);
  }

  useEffect(() => {
    decodeJwt();
    GetMyPostedJobs(0);
  }, []);
  const [viewAppliedUsers, setViewAppliedUsers] = useState(false);
  return (
    <div className="JobsContainer">
      {!viewAppliedUsers ? (
        <div className="JobsTable">
          {!isLoading && data && data.length > 0 ? (
            <div className="JobsGrid">
              <div className="Jobs">
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
                  className="loadMoreBtn job-item"
                  style={{ borderBottom: "0px" }}
                >
                  {noMoreData ? (
                    ""
                  ) : (
                    <div
                      onClick={() => {
                        console.log("hi");
                        setSkip(skip + 10);
                        GetMyPostedJobs(skip + 10);
                      }}
                    >
                      {!isLoading ? (
                        <h1>Load More</h1>
                      ) : (
                        <div style={{ maxWidth: "100%", maxHeight: "100%" }}>
                          <LoadingSpin />
                        </div>
                      )}
                    </div>
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
                            <button
                              style={{ width: "200px" }}
                              className="applyToJobBtn viewAppliedUsersBtn"
                              onClick={() => {
                                setViewAppliedUsers(true);
                              }}
                            >
                              {" "}
                              View who applied
                            </button>
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
      ) : (
        <div>
          {" "}
          <button
            style={{ marginBottom: "10px", height: "40px", width: "100px" }}
            onClick={() => setViewAppliedUsers(false)}
          >
            Go Back
          </button>
          <AppliedUsersTable jobId={selectedJob.jobId} />
        </div>
      )}
    </div>
  );
}
