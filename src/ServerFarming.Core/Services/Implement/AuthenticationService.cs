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
using System.Security.Claims;

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

        public UserInfo GetUserInfo()
        {
            long userId = GetUserId();
            return userRepository.GetUserInfo(userId);
        }

        async Task IAuthenticationService.Signin(LoginData loginData)
        {
            var signInResult = await signInManager.PasswordSignInAsync(loginData.Email, loginData.Password,
                isPersistent: true,
                lockoutOnFailure: false);

            if (!signInResult.Succeeded)
            {
                throw new LoginException("Wrong email/password");
            }
        }

        async Task<UserUpdateInfo> IAuthenticationService.UpdateUserInfo(UserUpdateInfo userInfo)
        {
            long userId = GetUserId();
            return await userRepository.UpdateUserInfo(userId, userInfo);
        }

        async Task IAuthenticationService.SignUp(RegisterCommand regCommand)
        {
            var user = new IdentityUser<long>()
            {
                Email = regCommand.Email,
                UserName = regCommand.Email,
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

        Task IAuthenticationService.SignOut()
        {
            return signInManager.SignOutAsync();
        }

        public long GetUserId()
        {
            var user = httpContextAccessor.HttpContext.User;
            var userIdString = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            long userId = 0;
            if (!string.IsNullOrEmpty(userIdString))
            {
                long.TryParse(userIdString, out userId);
            }
            return userId;
        }
    }
}
