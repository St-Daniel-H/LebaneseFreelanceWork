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
        Task<User> GetUserByName(string name);
        Task<User> GetUserByGoogleId(string googleId);
        Task<IEnumerable<User>> GetUsersFromJobId(int jobId);
        Task<IEnumerable<User>> GetAllUsers();

    }
}
