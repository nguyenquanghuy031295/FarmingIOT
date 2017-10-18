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
    /// <summary>
    /// DeviceController use for user need to watch environment data
    /// Or Kit send data to get actuator-action signal
    /// </summary>
    [Route("api/devices")]
    [Authorize]
    public class DeviceController : Controller
    {
        private readonly IDeviceService deviceService;
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="deviceService">used by DI</param>
        public DeviceController(IDeviceService deviceService)
        {
            this.deviceService = deviceService;
        }

        /// <summary>
        /// API for Kit send data and get actuator-action signal
        /// </summary>
        /// <param name="sensorRecord"></param>
        /// <returns>List Actuator-Action Signals</returns>
        [HttpPost("sendDataSensor")]
        [AllowAnonymous]
        public IActionResult sendDataSensor([FromBody]Sensor_Record sensorRecord)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var listAction = deviceService.SendSensorData(sensorRecord);
            return Ok(listAction);
        }

        /// <summary>
        /// API for getting environment data
        /// </summary>
        /// <param name="farmComponentId"></param>
        /// <returns>List Environment Data</returns>
        [HttpGet("getDataSensor/{farmComponentId}")]
        public IActionResult getDataSensor(long farmComponentId = 0)
        {
            var sensorData = deviceService.GetSensorData(farmComponentId);
            return Ok(sensorData);
        }
    }
}
