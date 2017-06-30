using FarmingDatabase.Model;
using ServerFarming.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFarming.Core.Repositories
{
    public interface ISensorRepository
    {
        List<Actuator_Action> AddNewSensorData(Sensor_Record sensorData);
        List<Sensor_Record> GetSensorData(long farmComponentID);
    }
}
