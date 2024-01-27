using LebUpwor.core.Models;

namespace LebUpwork.Api.Interfaces
{
    public interface IAppliedToTaskService
    {
        Task<IEnumerable<AppliedToTask>> GetUsersAppliedByTaskId (int id);
        Task<IEnumerable<AppliedToTask>> GetJobsApplied(int userId);
        Task<AppliedToTask> CreateAppliedToTask(AppliedToTask taskapp);
    }
}
