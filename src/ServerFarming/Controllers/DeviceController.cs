using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FarmingDatabase.Model;
using ServerFarming.Core.Services;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerFarming.Controllers
{
    [Route("api/devices")]
    [Authorize]
    public class DeviceController : Controller
    {
        private readonly IDeviceService deviceService;
        public DeviceController(IDeviceService deviceService)
        {
            this.deviceService = deviceService;
        }
        [HttpPost("sendDataSensor")]
        [AllowAnonymous]
        public IActionResult sendDataSensor([FromBody]Sensor_Record sensorRecord)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var listAction = deviceService.SendSensorData(sensorRecord);
            return Ok(listAction);
        }
        [HttpGet("getDataSensor/{farmComponentId}")]
        public IActionResult getDataSensor(long farmComponentId = 0)
        {
            var sensorData = deviceService.GetSensorData(farmComponentId);
            return Ok(sensorData);
        }
    }
}
