using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmingDatabase.Model;
using Microsoft.EntityFrameworkCore;
using FarmingDatabase.DatabaseContext;

namespace ServerFarming.Core.Repositories.Implement
{
    public class UserRepository : IUserRepository
    {
        private readonly FarmingDbContext _context;
        public UserRepository(FarmingDbContext context)
        {
            this._context = context;
        }
        void IUserRepository.AddNewUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
