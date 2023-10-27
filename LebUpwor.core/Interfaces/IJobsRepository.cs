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
    public interface IJobsRepository : IRepositoryRepository<Job>
    {
        Task<IEnumerable<Job>> GetAllJobs();
        Task<IEnumerable<Job>> GetJobsWithKeyword(string keywoard);
        Task<IEnumerable<Job>> GetJobById(int jobId);
        Task<IEnumerable<Job>> GetAllJobsPostedByUser(int userId);
        Task<IEnumerable<Job>> GetAllJobsFinishedByUser(int userId);
        Task<IEnumerable<Job>> GetAllFinishedJobsPostedByUser(int userId);

    }
}
