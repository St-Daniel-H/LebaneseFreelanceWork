using LebUpwor.core.DTO;
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
    public interface IAppliedToTaskRepository : IRepositoryRepository<AppliedToTask>
    {
        Task<IEnumerable<AppliedUsersDTO>> GetAllUsersWithTaskId(int taskId);
        Task<IEnumerable<AppliedToTask>> GetAllJobsWithUserId(int userId);
    }
}
