using LebUpwor.core.Models;
using Microsoft.AspNetCore.Identity;
using startup.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LebUpwor.core.Interfaces
{
    public interface IAppliedToTasksRepository : IRepositoryRepository<AppliedToTask>
    {
        Task<IEnumerable<AppliedToTask>> GetAllUsersWithTaskId(int taskId);
        Task<IEnumerable<AppliedToTask>> GetAllJobsWithUserId(int userId);
    }
}
