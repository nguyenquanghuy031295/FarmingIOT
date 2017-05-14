using FarmingDatabase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFarming.Core.Services
{
    public interface IDeviceService
    {
        Sensor_Record SendSensorData(Sensor_Record sensorData);
        List<Sensor_Record> GetSensorData();
    }
}
