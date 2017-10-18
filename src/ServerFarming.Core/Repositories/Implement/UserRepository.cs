using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmingDatabase.Model;
using Microsoft.EntityFrameworkCore;
using FarmingDatabase.DatabaseContext;
using ServerFarming.Core.Model;
using ServerFarming.Core.Command;

namespace ServerFarming.Core.Repositories.Implement
{
    /// <summary>
    /// UserRepository used for authenticating a user...
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly FarmingDbContext _context;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">used by DI</param>
        public UserRepository(FarmingDbContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Get ID of current User
        /// </summary>
        /// <param name="loginData"></param>
        /// <returns></returns>
        public long GetUserID(LoginData loginData)
        {
            var user = _context.Users.SingleOrDefault(userDB => (userDB.Email == loginData.Email));
            return user.UserId;
        }

        /// <summary>
        /// Get information of current Users from database
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserInfo GetUserInfo(long userId)
        {
            var user = _context.Users.Where(data => data.UserId == userId).SingleOrDefault();
            if (user != null)
            {
                return new UserInfo
                {
                    Name = user.Name,
                    Address = user.Address,
                    DOB = user.DOB,
                    Email = user.Email
                };
            }
            throw new Exception();
        }

        /// <summary>
        /// Update information of user in database
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public async Task<UserInfo> UpdateUserInfo(long userId, UserUpdateInfo userInfo)
        {
            var user = this._context.Users.Where(data => data.UserId == userId).SingleOrDefault();
            if (user != null)
            {
                user.Name = userInfo.Name;
                user.Address = userInfo.Address;
                user.DOB = userInfo.DOB;

                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new UserInfo
                {
                    Name = user.Name,
                    Address = user.Address,
                    DOB = user.DOB,
                    Email = user.Email
                };
            }
            throw new Exception();
        }

        /// <summary>
        /// Add a new user into database
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="regCommand"></param>
        /// <returns></returns>
        async Task<User> IUserRepository.AddNewUser(long userId, RegisterCommand regCommand)
        {
            var newUser = new User
            {
                UserId = userId,
                Email = regCommand.Email,
                Name = regCommand.Name
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }
    }
}
