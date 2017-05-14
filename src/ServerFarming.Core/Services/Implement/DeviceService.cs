using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmingDatabase.Model;
using ServerFarming.Core.Repositories;

namespace ServerFarming.Core.Services.Implement
{
    public class DeviceService : IDeviceService
    {
        private readonly ISensorRepository sensorRepository;
        public DeviceService(ISensorRepository sensorRepository)
        {
            this.sensorRepository = sensorRepository;
        }
        public Sensor_Record SendSensorData(Sensor_Record data)
        {
            var sensorData = CopyFromSensorData(data);
            sensorRepository.AddNewSensorData(sensorData);
            return sensorData;
        }

        public List<Sensor_Record> GetSensorData()
        {
            var listSensorData = sensorRepository.GetSensorData();
            return listSensorData;
        }
        private Sensor_Record CopyFromSensorData(Sensor_Record data)
        {
            return new Sensor_Record()
            {
                pH = data.pH,
                Air_Humidity = data.Air_Humidity,
                Ground_Humidity = data.Ground_Humidity,
                InsensityLight = data.InsensityLight,
                Temperature = data.Temperature,
                Timestamp = data.Timestamp,
                Wind = data.Wind,
                Farm_ComponentId = data.Farm_ComponentId
            };
        }
    }
}
