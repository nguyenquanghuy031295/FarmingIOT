using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FarmingDatabase.Model;
using ServerFarming.Core.Services;
using ServerFarming.Core.Model;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerFarming.Controllers
{
    [Route("api/farms")]
    public class FarmController : Controller
    {
        private readonly IFarmService farmService;
        private readonly IPlantService plantService;
        public FarmController(IFarmService farmService,
            IPlantService plantService)
        {
            this.farmService = farmService;
            this.plantService = plantService;
        }
        [HttpPost("newFarm")]
        public IActionResult AddNewFarm([FromBody]Farm farm)
        {
            //var list = _context.Farms.Include(f => f.User).ToList();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var newFarm = farmService.AddFarm(farm);
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
        public IActionResult GetFarmByUserID(long userID = 0)
        {
            var listFarms = farmService.GetFarmByUserID(userID);
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
