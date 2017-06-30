using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmingDatabase.Model;
using ServerFarming.Core.Repositories;
using ServerFarming.Core.Model;

namespace ServerFarming.Core.Services.Implement
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository userRepository;
        public AuthenticationService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public long GetUserID(LoginData loginData)
        {
            return userRepository.GetUserID(loginData);
        }

        public UserInfo GetUserInfo(long userId)
        {
            return userRepository.GetUserInfo(userId);
        }

        public bool Signin(LoginData loginData)
        {
            return userRepository.CheckSignin(loginData);
        }

        public UserUpdateInfo UpdateUserInfo(UserUpdateInfo userInfo)
        {
            return userRepository.UpdateUserInfo(userInfo);
        }

        MessageRegister IAuthenticationService.SignUp(User user)
        {
            return userRepository.AddNewUser(user);
        }
    }
}
