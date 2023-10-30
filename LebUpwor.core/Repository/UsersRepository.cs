using LebUpwor.core.Interfaces;
using LebUpwor.core.Models;
using Microsoft.EntityFrameworkCore;
using startup.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwor.core.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(UpworkLebContext context)
           : base(context)
        { }
        //Task<User> getUserByEmail(string email);
        //Task<User> getUserById(int id);
        //Task<User> getUserByName(string name);
        //Task<User> getUserByGoogleId(string googleId);
        //Task<IEnumerable<User>> GetUsersFromJobId(int jobId);
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await UpworkLebContext.Users
                .ToListAsync();
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await UpworkLebContext.Users
                 .Where(user => user.Email == email)
                .SingleOrDefaultAsync();
        }
        public async Task<User> GetUserById(int id)
        {
            return await UpworkLebContext.Users
                 .Where(user => user.UserId == id)
                .SingleOrDefaultAsync();
        }
        public async Task<User> GetUserByGoogleId(int id)
        {
            return await UpworkLebContext.Users
                 .Where(user => user.googleAccountId == id)
                .SingleOrDefaultAsync();
        }
        public async Task<IEnumerable<User>> GetUserByName(string Name)
        {
            return await UpworkLebContext.Users
                .Where(user => (user.FirstName + " " + user.LastName).Contains(Name))
                .ToListAsync();
        }


        private UpworkLebContext UpworkLebContext
        {
            get { return Context as UpworkLebContext; }
        }
    }
}
