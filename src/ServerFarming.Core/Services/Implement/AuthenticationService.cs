using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmingDatabase.Model;
using ServerFarming.Core.Repositories;

namespace ServerFarming.Core.Services.Implement
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository userRepository;
        public AuthenticationService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        bool IAuthenticationService.SignUp(User user)
        {
            try
            {
                userRepository.AddNewUser(user);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
