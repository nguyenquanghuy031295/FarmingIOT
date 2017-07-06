﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServerFarming.Core.Services;
using FarmingDatabase.Model;
using ServerFarming.Core.Model;
using ServerFarming.Core.Command;
using ServerFarming.Core.Exceptions;

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

        [HttpPost("signin")]
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
                return BadRequest(e.Message);
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
                return BadRequest(e.Message);
            }
        }
    }
}
