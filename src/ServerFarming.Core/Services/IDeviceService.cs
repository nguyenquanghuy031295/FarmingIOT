using FarmingDatabase.Model;
using ServerFarming.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFarming.Core.Services
{
    public interface IDeviceService
    {
        List<Actuator_Action> SendSensorData(Sensor_Record sensorData);
        List<Sensor_Record> GetSensorData(long farmComponentID);
    }
}
