using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServerFarming.Core.Services;
using Microsoft.AspNetCore.Authorization;
using ServerFarming.Core.Exceptions;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerFarming.Controllers
{
    [Route("api/plants")]
    [Authorize]
    public class PlantController : Controller
    {
        private readonly IPlantService plantService;
        public PlantController(IPlantService plantService)
        {
            this.plantService = plantService;
        }
        [HttpGet("detail/{farmComponentId}")]
        public IActionResult GetPlantDetail(long farmComponentId = 0)
        {
            var listPlantDetail = plantService.GetPlantDetail(farmComponentId);
            return Ok(listPlantDetail);
        }
        [HttpGet]
        public IActionResult GetAllPlant()
        {
            var listPlant = plantService.GetAllPlant();
            return Ok(listPlant);
        }
        [HttpGet("changePeriod/{farmComponentId}")]
        public IActionResult AskChangePeriod(long farmComponentId)
        {
            var signal = plantService.AskChangePeriod(farmComponentId);
            return Ok(signal);
        }
        [HttpGet("nextPeriod/{farmComponentId}")]
        public IActionResult GetNextPeriodDetail(long farmComponentId)
        {
            try
            {
                var nextPeriod = plantService.GetNextPeriodDetail(farmComponentId);
                return Ok(nextPeriod);
            }
            catch (ChangePeriodException ex)
            {
                return BadRequest();
            }
            catch(UnAuthorizedException ex)
            {
                return BadRequest();
            }
        }
        [HttpPost("changePeriod/{farmComponentId}")]
        public async Task<IActionResult> ChangePeriod(long farmComponentId)
        {
            var isOwned = plantService.CheckUserHaveFarmComponent(farmComponentId);
            if (isOwned)
            {
                await plantService.ChangeNextPeriod(farmComponentId);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
