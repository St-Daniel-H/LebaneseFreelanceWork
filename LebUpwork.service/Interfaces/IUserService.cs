using LebUpwor.core.Models;

namespace LebUpwork.Api.Interfaces
{
    public interface IUserService
    {
        //GET
        Task<IEnumerable<User>> GetAllUsers();

        Task<User> GetUserByEmail(String Email);
        Task<User> GetUserByGoogleId(int googleId);
        Task<User> GetUserById(int Id);
        //CURD
        Task<User> CreateUser(User user);
        Task DeleteUser(User user);
        Task UpdateUser(User userToBeUpdated, User user);
        //wallet stuff
        Task SendTokens(User from, User to,double amount);

        //end of wallet stuff
        string HashPassword(string password, out string salt);
        bool CheckPassword(User user,string password);

    }
}
