using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FarmingDatabase.Model;
using ServerFarming.Core.Services;
using ServerFarming.Core.Model;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerFarming.Controllers
{
    [Route("api/farms")]
    [Authorize]
    public class FarmController : Controller
    {
        private readonly IFarmService farmService;
        private readonly IPlantService plantService;
        private readonly IAuthenticationService authenticationService;
        public FarmController(IFarmService farmService,
            IPlantService plantService,
            IAuthenticationService authenticationService)
        {
            this.farmService = farmService;
            this.plantService = plantService;
            this.authenticationService = authenticationService;
        }
        [HttpPost("newFarm")]
        public async Task<IActionResult> AddNewFarm([FromBody]FarmCommand farmCommand)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            long userId = authenticationService.GetUserId();
            var newFarm = await farmService.AddFarm(userId, farmCommand);
            return Ok(newFarm);
        }
        [HttpPost("newFarmComponent")]
        public IActionResult AddNewFarmComponent([FromBody]FarmingComponentDTO farmComponentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var newFarmComponent = farmService.AddFarmComponent(farmComponentDTO);
            var newPlant = plantService.AddPlant(farmComponentDTO, newFarmComponent.Farm_ComponentId);
            return Ok(newFarmComponent);
        }

        [HttpGet("getUserFarms")]
        public async Task<IActionResult> GetUserFarms()
        {
            long userId = authenticationService.GetUserId();
            var listFarms = await farmService.GetUserFarms(userId);
            return Ok(listFarms);
        }

        [HttpGet("getFarmComponents")]
        public IActionResult GetFarmComponentsByFarmID(long farmID = 0)
        {
            var listFarmComponents = farmService.GetFarmComponents(farmID);
            return Ok(listFarmComponents);
        }

        [HttpGet("report/overallmonth/{farmComponentId}")]
        public IActionResult GetOverallEnvironmentMonth(long farmComponentId)
        {
            var overallEnvInfo = farmService.GetOverallEnvironmentInfo(farmComponentId);
            return Ok(overallEnvInfo);
        }

        [HttpGet("report/today/{farmComponentId}")]
        public IActionResult GetEnvInfoToday(long farmComponentId)
        {
            var listEnvInfoToday = farmService.GetEnvInfoToday(farmComponentId);
            return Ok(listEnvInfoToday);
        }
    }
}
