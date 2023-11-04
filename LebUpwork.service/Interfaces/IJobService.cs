using LebUpwor.core.Models;

namespace LebUpwork.Api.Interfaces
{
    public interface IJobService
    {
        Task<Job> GetJobWithJobId(int  jobId);
        Task<Job> CreateJob(Job job);
        Task<Job> UpdateJob(Job jobToUpdate, Job job);
        Task<Job> DeleteJob(int jobId);
        Task<Job> FinishedJob(Job job);

    }
}
