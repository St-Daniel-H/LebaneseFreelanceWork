using LebUpwor.core.DTO;
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
    public class NewJobRepository : Repository<NewJob>, INewJobRepository
    {
        public NewJobRepository(UpworkLebContext context) : base(context)
        { }
        private UpworkLebContext UpworkLebContext
        {
            get { return Context as UpworkLebContext; }
        }

        public async Task<IEnumerable<NewJob>> GetAllJobTracks()
        {
            return await UpworkLebContext.NewJobs
                .Include(u=> u.User)
                .Include(u=> u.Job)
                .ToListAsync();
        }

        public async Task<NewJob> GetJobTrackerByIds(int jobId)
        {
            return UpworkLebContext.NewJobs
                .Where(u => u.JobId == jobId)
                .SingleOrDefault();
        }
        public async Task<NewJobDTO> GetJobTrackerByIdWithUser(int jobId)
        {
            return UpworkLebContext.NewJobs
                .Where(u => u.JobId == jobId)
                .Select(u => new NewJobDTO
                {
                    User = new UserWithTokensDTO{
                     Token = u.User.Token
                    },
                    JobId = u.JobId,
                    date = u.Date,
                }
                )
                .SingleOrDefault();
        }
    }
}
