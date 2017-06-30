using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmingDatabase.Model;
using Microsoft.EntityFrameworkCore;
using FarmingDatabase.DatabaseContext;
using ServerFarming.Core.Model;

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
            var flag = _context.Users.SingleOrDefault(userDB => (userDB.Email == loginData.Email) && (userDB.Password == loginData.Password));
            if (flag != null)
                return true;
            return false;
        }

        public long GetUserID(LoginData loginData)
        {
            var user = _context.Users.SingleOrDefault(userDB => (userDB.Email == loginData.Email) && (userDB.Password == loginData.Password));
            return user.UserId;
        }

        public UserInfo GetUserInfo(long userId)
        {
            var user = _context.Users.Where(data => data.UserId == userId).SingleOrDefault();
            if(user != null)
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

        public UserUpdateInfo UpdateUserInfo(UserUpdateInfo userInfo)
        {
            var user = this._context.Users.Where(data => data.UserId == userInfo.UserId).SingleOrDefault();
            if (user != null)
            {
                user.Name = userInfo.Name;
                user.Address = userInfo.Address;
                user.DOB = userInfo.DOB;

                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();
                return new UserUpdateInfo
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    Address = user.Address,
                    DOB = user.DOB
                };
            }
            throw new Exception();
        }

        MessageRegister IUserRepository.AddNewUser(User user)
        {
            var a = _context.Users.FirstOrDefault(userDB => userDB.Email == user.Email);
            if(a != null)
            {
                return new MessageRegister()
                {
                    IsSuccess = false,
                    Message = "Email is existed"
                };
            }
            _context.Users.Add(user);
            _context.SaveChanges();
            return new MessageRegister()
            {
                IsSuccess = true,
                Message = "Register Successfully"
            };
        }
    }
}
