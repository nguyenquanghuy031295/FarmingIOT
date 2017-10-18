using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServerFarming.Core.Services;
using FarmingDatabase.Model;
using ServerFarming.Core.Model;
using ServerFarming.Core.Command;
using ServerFarming.Core.Exceptions;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerFarming.Controllers
{
    /// <summary>
    /// AuthenticationController use for authenticating user log in
    /// </summary>
    [Route("api/authentication")]
    [Authorize]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService authenticationService;
        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="authenticationService">used by DI</param>
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        /// <summary>
        /// API for registering new account
        /// </summary>
        /// <param name="regCommand"></param>
        /// <returns>Status Code : 200 / 401</returns>
        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody]RegisterCommand regCommand)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await authenticationService.SignUp(regCommand);
                return Ok();
            }
            catch (RegisterException e)
            {
                return BadRequest(e.ErrorMessages);
            }
        }

        /// <summary>
        /// API for user log in
        /// </summary>
        /// <param name="loginData"></param>
        /// <returns>Status Code: 200 / 401</returns>
        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> Signin([FromBody]LoginData loginData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await authenticationService.Signin(loginData);
                return Ok();
            }catch (LoginException e)
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// API for updating information of user
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns>Information of User</returns>
        [HttpPost("accountInfo")]
        public async Task<IActionResult> UpdateAccountInfo([FromBody]UserUpdateInfo userInfo)
        {
            try
            {
                var info = await authenticationService.UpdateUserInfo(userInfo);
                return Ok(info);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// API for getting information of user
        /// </summary>
        /// <returns>Information of User</returns>
        [HttpGet("accountInfo")]
        public IActionResult GetAccountInfo()
        {
            try
            {
                var userInfo = authenticationService.GetUserInfo();
                return Ok(userInfo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// API for logging out
        /// </summary>
        /// <returns>Status Code: 200</returns>
        [HttpPost("signout")]
        public async Task<IActionResult> SignOut()
        {
            await authenticationService.SignOut();
            return Ok();
        }

        /// <summary>
        /// API for user change the password of his/her account
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status Code: 200 / 401</returns>
        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await authenticationService.ChangePassword(
                    command.OldPassword,
                    command.NewPassword);
            }
            catch (ChangePasswordException ex)
            {
                return BadRequest(ex.ErrorMessages);
            }

            return Ok();
        }
    }
}
