using FarmingDatabase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFarming.Core.Repositories
{
    public interface ISensorRepository
    {
        void AddNewSensorData(Sensor_Record sensorData);
        List<Sensor_Record> GetSensorData(long farmComponentID);
    }
}
