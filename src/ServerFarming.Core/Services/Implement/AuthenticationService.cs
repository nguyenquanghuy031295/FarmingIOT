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
    /// <summary>
    /// AuthenticationService used for handing data for authenticating a user
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository userRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<IdentityUser<long>> userManager;
        private readonly SignInManager<IdentityUser<long>> signInManager;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userRepository">used by DI</param>
        /// <param name="httpContextAccessor">used by DI</param>
        /// <param name="userManager">used by DI</param>
        /// <param name="signInManager">used by DI</param>
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

        /// <summary>
        /// Get infomation of current User
        /// </summary>
        /// <returns></returns>
        public UserInfo GetUserInfo()
        {
            long userId = GetUserId();
            return userRepository.GetUserInfo(userId);
        }

        /// <summary>
        /// user log in
        /// </summary>
        /// <param name="loginData"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Current User update his/her information
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        async Task<UserInfo> IAuthenticationService.UpdateUserInfo(UserUpdateInfo userInfo)
        {
            long userId = GetUserId();
            return await userRepository.UpdateUserInfo(userId, userInfo);
        }

        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="regCommand"></param>
        /// <returns></returns>
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

        /// <summary>
        /// User sign out
        /// </summary>
        /// <returns></returns>
        Task IAuthenticationService.SignOut()
        {
            return signInManager.SignOutAsync();
        }

        /// <summary>
        /// Get ID of current User
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Change password of current user
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        async Task IAuthenticationService.ChangePassword(string oldPassword, string newPassword)
        {
            var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);

            var result = await userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new ChangePasswordException(errors);
            }
        }
    }
}
