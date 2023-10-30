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
    public class AppliedToTaskRepository : Repository<AppliedToTask>, IAppliedToTaskRepository
    {
        //Task<IEnumerable<AppliedToTask>> GetAllUsersWithTaskId(int taskId);
        //Task<IEnumerable<AppliedToTask>> GetAllJobsWithUserId(int userId);
        public AppliedToTaskRepository(UpworkLebContext context)
        : base(context)
        { }
        private UpworkLebContext UpworkLebContext
        {
            get { return Context as UpworkLebContext; }
        }
        public async Task<IEnumerable<AppliedToTask>> GetAllUsersWithTaskId(int taskId)
        {
            return await UpworkLebContext.AppliedToTasks
                .Where(a => a.JobId == taskId)
                .ToListAsync();
        }
        public async Task<IEnumerable<AppliedToTask>> GetAllJobsWithUserId(int userId)
        {
            return await UpworkLebContext.AppliedToTasks
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }
    }
}
