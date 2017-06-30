using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServerFarming.Core.Services;
using FarmingDatabase.Model;
using ServerFarming.Core.Model;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerFarming.Controllers
{
    [Route("api/authentication")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }
        [HttpPost("signup")]
        public IActionResult SignUp([FromBody]User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var signal = authenticationService.SignUp(user);
            if (signal.IsSuccess)
                return Ok();
            else
                return BadRequest(signal.Message);
        }

        [HttpPost("signin")]
        public IActionResult Signin([FromBody]LoginData loginData)
        {
            bool isSuccess = authenticationService.Signin(loginData);
            if (isSuccess)
            {
                long userID = authenticationService.GetUserID(loginData);
                return Ok(new DataId { ID = userID});
            }
            return Unauthorized();
        }

        [HttpPost("accountInfo")]
        public IActionResult UpdateAccountInfo([FromBody]UserUpdateInfo userInfo)
        {
            try
            {
                var info = authenticationService.UpdateUserInfo(userInfo);
                return Ok(info);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpGet("accountInfo")]
        public IActionResult GetAccountInfo(long userId)
        {
            try
            {
                var userInfo = authenticationService.GetUserInfo(userId);
                return Ok(userInfo);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
