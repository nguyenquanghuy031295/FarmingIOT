using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FarmingDatabase.Model;
using ServerFarming.Core.Services;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerFarming.Controllers
{
    [Route("api/devices")]
    public class DeviceController : Controller
    {
        private readonly IDeviceService deviceService;
        public DeviceController(IDeviceService deviceService)
        {
            this.deviceService = deviceService;
        }
        [HttpPost("sendDataSensor")]
        public IActionResult sendDataSensor([FromBody]Sensor_Record sensorRecord)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var listAction = deviceService.SendSensorData(sensorRecord);
            return Ok(listAction);
        }
        [HttpGet("getDataSensor")]
        public IActionResult getDataSensor(long farmComponentID = 0)
        {
            var sensorData = deviceService.GetSensorData(farmComponentID);
            return Ok(sensorData);
        }
    }
}
