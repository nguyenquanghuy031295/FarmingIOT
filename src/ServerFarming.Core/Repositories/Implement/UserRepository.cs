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
