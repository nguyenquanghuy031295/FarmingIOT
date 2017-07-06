using FarmingDatabase.Model;
using ServerFarming.Core.Command;
using ServerFarming.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFarming.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddNewUser(long userId, RegisterCommand regCommand);
        Boolean CheckSignin(LoginData loginData);
        long GetUserID(LoginData loginData);
        Task<UserUpdateInfo> UpdateUserInfo(long userId, UserUpdateInfo userInfo);
        UserInfo GetUserInfo(long userId);
    }
}
