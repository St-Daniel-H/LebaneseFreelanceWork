using LebUpwor.core.DTO;
using LebUpwor.core.Models;
using startup.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace LebUpwor.core.Interfaces
{
    public interface IJobRepository : IRepositoryRepository<Job>
    {
        Task<IEnumerable<Job>> GetAllJobs();
        Task<Job> GetJobById(int jobid);
        Task<IEnumerable<JobDTO>> GetJobsWithTag(ICollection<string> tagStrings, int skip, int pageSize);
        Task<IEnumerable<JobDTO>> GetJobsWithKeyword(string keyword, int skip, int pageSize);

        Task<IEnumerable<JobDTO>> GetAllJobsPostedByUser(int userId,int skip, int page);
        Task<IEnumerable<JobDTO>> GetAllJobsFinishedByUser(int userId, int skip, int page);
        Task<IEnumerable<JobDTO>> GetAllFinishedJobsPostedByUser(int userId, int skip, int page);
        Task<JobWithAppliedUsersDTO> GetJobByIdIncludeAppliedToTasks(int Jobid);

    }
}
