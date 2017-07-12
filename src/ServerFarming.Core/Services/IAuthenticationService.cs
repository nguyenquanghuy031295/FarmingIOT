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
        Task<UserInfo> UpdateUserInfo(UserUpdateInfo userInfo);
        UserInfo GetUserInfo();
        Task SignOut();
        long GetUserId();
    }
}
