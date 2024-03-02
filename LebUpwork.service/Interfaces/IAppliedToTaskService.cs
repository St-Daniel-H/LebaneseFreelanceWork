using LebUpwor.core.DTO;
using LebUpwor.core.Models;

namespace LebUpwork.Api.Interfaces
{
    public interface IAppliedToTaskService
    {
        Task<IEnumerable<AppliedUsersDTO>> GetUsersAppliedByTaskId (int id);
        Task<IEnumerable<AppliedUsersDTO>> GetJobsApplied(int userId);
        Task<AppliedToTask> CreateAppliedToTask(AppliedToTask taskapp);
    }
}
