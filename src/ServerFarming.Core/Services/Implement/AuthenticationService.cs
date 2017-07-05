using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmingDatabase.Model;
using ServerFarming.Core.Repositories;
using ServerFarming.Core.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using ServerFarming.Core.Command;
using ServerFarming.Core.Exceptions;

namespace ServerFarming.Core.Services.Implement
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository userRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<IdentityUser<long>> userManager;
        private readonly SignInManager<IdentityUser<long>> signInManager;
        public AuthenticationService(IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor,
            UserManager<IdentityUser<long>> userManager,
            SignInManager<IdentityUser<long>> signInManager)
        {
            this.userRepository = userRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.signInManager = signInManager;
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

        async Task IAuthenticationService.SignUp(RegisterCommand regCommand)
        {
            var user = new IdentityUser<long>()
            {
                Email = regCommand.Email,
                UserName = regCommand.Name,
                EmailConfirmed = true
            };

            var createUserResult = await userManager.CreateAsync(user, regCommand.Password);

            if (!createUserResult.Succeeded)
            {
                var messages = createUserResult.Errors.Select(e => e.Description);
                throw new RegisterException(messages);
            }
            else
            {
                await userRepository.AddNewUser(user.Id, regCommand);
            }
        }
    }
}
