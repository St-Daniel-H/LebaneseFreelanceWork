using LebUpwor.core.Models;

namespace LebUpwork.Api.Interfaces
{
    public interface IAppliedToTaskService
    {
        Task<AppliedToTask> GetAppliedToTaskService(int id);
        Task<AppliedToTask> AddUser(int userId);
    }
}
