using LebUpwor.core.Models;

namespace LebUpwork.Api.Interfaces
{
    public interface IRoleService
    {
        Task<Role> CreateRole(Role role);
        Task DeleteRole(Role role);
        Task<Role> UpdateRole(Role role, Role newRole);
        Task<IEnumerable<Role>> GetRoles(Role role);
    }
}
