using FarmingDatabase.Model;
using ServerFarming.Core.Command;
using ServerFarming.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFarming.Core.Services
{
    public interface IAuthenticationService
    {
        Task SignUp(RegisterCommand regCommand);
        Task Signin(LoginData loginData);
        long GetUserID(LoginData loginData);
        UserUpdateInfo UpdateUserInfo(UserUpdateInfo userInfo);
        UserInfo GetUserInfo(long userId);
    }
}
