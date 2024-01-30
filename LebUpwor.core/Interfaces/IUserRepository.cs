using LebUpwor.core.Models;
using startup.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwor.core.Interfaces
{
    public interface IUserRepository : IRepositoryRepository<User>
    {

        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(int id);
        Task<IEnumerable<User>> GetUserByName(string name);
        Task<User> GetUserByGoogleId(int googleId);
        Task<IEnumerable<User>> GetAllUsers();

    }
}
