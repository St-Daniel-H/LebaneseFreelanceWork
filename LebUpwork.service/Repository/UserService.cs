using BookingSystem.core.Repository;
using LebUpwork.Api.Interfaces;
using LebUpwor.core.Models;
using LebUpwor.core.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace LebUpwork.Api.Repository
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _unitOfWork.Users.GetAllUsers();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _unitOfWork.Users.GetUserByEmail(email);
        }
        public async Task<User> GetUserByGoogleId(int googleId)
        {
            return await _unitOfWork.Users.GetUserByGoogleId(googleId);
        }
        public async Task<User> GetUserById(int Id)
        {
            return await _unitOfWork.Users.GetUserById(Id);
        }
        public async Task<User> CreateUser(User NewUser)
        {
            await _unitOfWork.Users.AddAsync(NewUser);
            await _unitOfWork.CommitAsync();
            return NewUser;
        }
        public async Task UpdateUser(User UserToBeUpdated, User user)
        {
            UserToBeUpdated.FirstName = user.FirstName;
            UserToBeUpdated.LastName = user.LastName;
            UserToBeUpdated.CVpdf = user.CVpdf;
            UserToBeUpdated.ProfilePicture = user.ProfilePicture;
            UserToBeUpdated.Email = user.Email;

            await _unitOfWork.CommitAsync();
        }
        public async Task SendTokens(User From, User To,double amount)
        {
            From.Token -= amount;
            To.Token += amount;

            await _unitOfWork.CommitAsync();
        }
        public async Task DeleteUser(User user)
        {
            _unitOfWork.Users.Remove(user);
            await _unitOfWork.CommitAsync();
        }
    }
}
