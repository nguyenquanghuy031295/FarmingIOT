using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmingDatabase.Model;
using FarmingDatabase.DatabaseContext;

namespace ServerFarming.Core.Repositories.Implement
{
    public class SensorRepository : ISensorRepository
    {
        private readonly FarmingDbContext farmingContext;
        public SensorRepository(FarmingDbContext farmingContext)
        {
            this.farmingContext = farmingContext;
        }
        public void AddNewSensorData(Sensor_Record sensorData)
        {
            farmingContext.SensorRecords.Add(sensorData);
            farmingContext.SaveChanges();
        }
        public List<Sensor_Record> GetSensorData()
        {
            return farmingContext.SensorRecords.Where(s=> true).ToList();
        }
    }
}
