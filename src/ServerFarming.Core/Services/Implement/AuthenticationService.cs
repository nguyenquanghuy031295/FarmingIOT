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

        public bool Signin(LoginData loginData)
        {
            return userRepository.CheckSignin(loginData);
        }

        MessageRegister IAuthenticationService.SignUp(User user)
        {
            return userRepository.AddNewUser(user);
        }
    }
}
