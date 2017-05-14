using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServerFarming.Core.Services;
using FarmingDatabase.Model;

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
            bool signal = authenticationService.SignUp(user);
            if (signal)
                return Ok();
            else
                return BadRequest();
        }
    }
}
