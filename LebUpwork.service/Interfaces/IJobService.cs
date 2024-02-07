using LebUpwor.core.DTO;
using LebUpwor.core.Models;

namespace LebUpwork.Api.Interfaces
{
    public interface IJobService
    {
        Task<Job> GetJobById(int  jobId);
        Task<Job> CreateJob(Job job);
        Task<Job> UpdateJob(Job jobToUpdate, Job job);
        Task<IEnumerable<JobDTO>> GetJobsWithTag(ICollection<string> tagStrings, int skip, int pageSize);
        Task<IEnumerable<JobDTO>> GetJobsWithKeyword(string keyword, int skip, int pageSize);

        Task DeleteJob(Job job);
        Task CommitChanges();

    }
}
