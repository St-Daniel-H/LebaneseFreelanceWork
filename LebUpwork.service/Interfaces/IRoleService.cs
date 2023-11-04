using LebUpwor.core.Models;

namespace LebUpwork.Api.Interfaces
{
    public interface IRoleService
    {
        Task<Role> CreateRole(Role role);
        Task<Role> DeleteRole(Role role);
    }
}
