using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServerFarming.Core.Services;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerFarming.Controllers
{
    [Route("api/plants")]
    public class PlantController : Controller
    {
        private readonly IPlantService plantService;
        public PlantController(IPlantService plantService)
        {
            this.plantService = plantService;
        }
        [HttpGet("detail")]
        public IActionResult GetPlantDetail(long farmComponentId = 0)
        {
            var listPlantDetail = plantService.GetPlantDetail(farmComponentId);
            return Ok(listPlantDetail);
        }
    }
}
