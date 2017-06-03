using FarmingDatabase.Model;
using ServerFarming.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFarming.Core.Repositories
{
    public interface IUserRepository
    {
        MessageRegister AddNewUser(User user);
        Boolean CheckSignin(LoginData loginData);
        long GetUserID(LoginData loginData);
    }
}
