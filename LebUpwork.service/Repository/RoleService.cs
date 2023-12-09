using LebUpwor.core.Interfaces;
using LebUpwor.core.Models;
using LebUpwork.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwork.service.Repository
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoleService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        //Task<Role> CreateRole(Role role);
        //Task<Role> DeleteRole(Role role);
        public async Task<Role> CreateRole(Role role)
        {
            await _unitOfWork.Roles.AddAsync(role);
            await _unitOfWork.CommitAsync();
            return role;
        }
        public async Task DeleteRole(Role role)
        {
             _unitOfWork.Roles.Remove(role);
            await _unitOfWork.CommitAsync();
        }
        public async Task<Role> UpdateRole(Role role, Role newRole)
        {
            role.RoleName = newRole.RoleName;
            await _unitOfWork.CommitAsync();
            return role;
        }
        public async Task<IEnumerable<Role>> GetRoles(Role role)
        {
            return await _unitOfWork.Roles.GetAllAsync();
        }
    }
}
