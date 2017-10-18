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
    /// <summary>
    /// PlantController use for get information current Plant
    /// </summary>
    [Route("api/plants")]
    [Authorize]
    public class PlantController : Controller
    {
        private readonly IPlantService plantService;
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="plantService">used by DI</param>
        public PlantController(IPlantService plantService)
        {
            this.plantService = plantService;
        }

        /// <summary>
        /// API for get detail of current plant in farm component
        /// </summary>
        /// <param name="farmComponentId"></param>
        /// <returns>Information of Plant</returns>
        [HttpGet("detail/{farmComponentId}")]
        public IActionResult GetPlantDetail(long farmComponentId = 0)
        {
            var listPlantDetail = plantService.GetPlantDetail(farmComponentId);
            return Ok(listPlantDetail);
        }

        /// <summary>
        /// API for asking system current plant can change to next Period?
        /// </summary>
        /// <param name="farmComponentId"></param>
        /// <returns>Status Code: 200 with value True / False</returns>
        [HttpGet("changePeriod/{farmComponentId}")]
        public IActionResult AskChangePeriod(long farmComponentId)
        {
            var signal = plantService.AskChangePeriod(farmComponentId);
            return Ok(signal);
        }

        /// <summary>
        /// API for getting information next Period of current Plant
        /// </summary>
        /// <param name="farmComponentId"></param>
        /// <returns>Information of next Period</returns>
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

        /// <summary>
        /// API for user want to change period of current Plant to next Period
        /// </summary>
        /// <param name="farmComponentId"></param>
        /// <returns></returns>
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
