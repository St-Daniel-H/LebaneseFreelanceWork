using LebUpwor.core.Interfaces;
using LebUpwor.core.Models;
using Microsoft.EntityFrameworkCore;
using startup.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwor.core.Repository
{
    public class JobRepository : Repository<Job>, IJobRepository
    {
        //Task<IEnumerable<Job>> GetAllJobs();
        //Task<IEnumerable<Job>> GetJobsWithKeyword(string keywoard);
        //Task<IEnumerable<Job>> GetJobById(int jobId);
        //Task<IEnumerable<Job>> GetAllJobsPostedByUser(int userId);
        //Task<IEnumerable<Job>> GetAllJobsFinishedByUser(int userId);
        //Task<IEnumerable<Job>> GetAllFinishedJobsPostedByUser(int userId);

        public JobRepository(UpworkLebContext context)
          : base(context)
        { }
        private UpworkLebContext UpworkLebContext
        {
            get { return Context as UpworkLebContext; }
        }
        public async Task<Job> GetJobById(int Jobid)
        {
            return await UpworkLebContext.Jobs
                 .Where(job => job.JobId == Jobid)
                .SingleOrDefaultAsync();
        }
        public async Task<IEnumerable<Job>> GetAllJobs()
        {
            return await UpworkLebContext.Jobs
              .ToListAsync();
        }
        public async Task<IEnumerable<Job>> GetJobsWithKeyword(string keywoard)
        {
            return await UpworkLebContext.Jobs
                .Where(j => j.Title.Contains(keywoard) || j.Description.Contains(keywoard))
                .ToListAsync();
        }
        public async Task<IEnumerable<Job>> GetAllJobsPostedByUser(int userId)
        {
            return await UpworkLebContext.Jobs
                .Where(j => j.UserId == userId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Job>> GetAllJobsFinishedByUser(int userId)
        {
            return await UpworkLebContext.Jobs
                .Where(j => j.FinishedByUserId == userId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Job>> GetAllFinishedJobsPostedByUser(int userId)
        {
            return await UpworkLebContext.Jobs
                .Where(j => j.UserId == userId && j.IsCompleted == true) .ToListAsync();
        }
    }
}
