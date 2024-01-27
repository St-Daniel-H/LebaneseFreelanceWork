using LebUpwor.core.Models;

namespace LebUpwork.Api.Interfaces
{
    public interface IJobService
    {
        Task<Job> GetJobById(int  jobId);
        Task<Job> CreateJob(Job job);
        Task<Job> UpdateJob(Job jobToUpdate, Job job);
        Task DeleteJob(Job job);
    }
}
