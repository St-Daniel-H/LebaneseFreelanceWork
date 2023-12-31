﻿using LebUpwor.core.Models;

namespace LebUpwork.Api.Interfaces
{
    public interface IUserService
    {
        //GET
        Task<User> GetUserByEmail(String Email);
        Task<User> GetUserByGoogleId(int googleId);
        Task<User> GetUserById(int Id);
        //CURD
        Task<User> CreateUser(User user);
        Task DeleteUser(User user);
        Task UpdateUser(User userToBeUpdated, User user);
        //wallet stuff
        Task SendDiamonds(User from, User to,double amount);
    
        //end of wallet stuff

        
    }
}
