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
    public class UserRepository : IUserRepository
    {
        private readonly FarmingDbContext _context;
        public UserRepository(FarmingDbContext context)
        {
            this._context = context;
        }

        public bool CheckSignin(LoginData loginData)
        {
            //var flag = _context.Users.SingleOrDefault(userDB => (userDB.Email == loginData.Email) && (userDB.Password == loginData.Password));
            //if (flag != null)
            //    return true;
            return false;
        }

        public long GetUserID(LoginData loginData)
        {
            var user = _context.Users.SingleOrDefault(userDB => (userDB.Email == loginData.Email));
            return user.UserId;
        }

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

        public async Task<UserUpdateInfo> UpdateUserInfo(long userId, UserUpdateInfo userInfo)
        {
            var user = this._context.Users.Where(data => data.UserId == userId).SingleOrDefault();
            if (user != null)
            {
                user.Name = userInfo.Name;
                user.Address = userInfo.Address;
                user.DOB = userInfo.DOB;

                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new UserUpdateInfo
                {
                    Name = user.Name,
                    Address = user.Address,
                    DOB = user.DOB
                };
            }
            throw new Exception();
        }

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
