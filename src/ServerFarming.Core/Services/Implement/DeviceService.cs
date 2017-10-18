using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmingDatabase.Model;
using ServerFarming.Core.Repositories;
using ServerFarming.Core.Model;

namespace ServerFarming.Core.Services.Implement
{
    /// <summary>
    /// DeviceService used for handling data related to a Sensor before saving into Database
    /// </summary>
    public class DeviceService : IDeviceService
    {
        private readonly ISensorRepository sensorRepository;
        public DeviceService(ISensorRepository sensorRepository)
        {
            this.sensorRepository = sensorRepository;
        }
        /// <summary>
        /// User send environment data and get list action of actuators
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<Actuator_Action> SendSensorData(Sensor_Record data)
        {
            var sensorData = CopyFromSensorData(data);
            var listAction = sensorRepository.AddNewSensorData(sensorData);
            return listAction;
        }

        /// <summary>
        /// Get Environment Data of current Farm Component
        /// </summary>
        /// <param name="farmComponentID"></param>
        /// <returns></returns>
        public List<Sensor_Record> GetSensorData(long farmComponentID)
        {
            var listSensorData = sensorRepository.GetSensorData(farmComponentID);
            return listSensorData;
        }

        /// <summary>
        /// Copy data need from command
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private Sensor_Record CopyFromSensorData(Sensor_Record data)
        {
            return new Sensor_Record()
            {
                pH = data.pH,
                Air_Humidity = data.Air_Humidity,
                Soil_Humidity = data.Soil_Humidity,
                Luminosity = data.Luminosity,
                Temperature = data.Temperature,
                Timestamp = data.Timestamp,
                Wind = data.Wind,
                Farm_ComponentId = data.Farm_ComponentId
            };
        }
    }
}
