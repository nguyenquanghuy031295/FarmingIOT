using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmingDatabase.Model;
using FarmingDatabase.DatabaseContext;
using ServerFarming.Core.Model;

namespace ServerFarming.Core.Repositories.Implement
{
    /// <summary>
    /// SensorRepository used for saving data of sensors into database
    /// </summary>
    public class SensorRepository : ISensorRepository
    {
        private readonly FarmingDbContext farmingContext;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="farmingContext">used by DI</param>
        public SensorRepository(FarmingDbContext farmingContext)
        {
            this.farmingContext = farmingContext;
        }

        /// <summary>
        /// Add new environment data into database
        /// </summary>
        /// <param name="sensorData"></param>
        /// <returns>List action of actuators</returns>
        public List<Actuator_Action> AddNewSensorData(Sensor_Record sensorData)
        {
            List<Actuator_Action> listAction = new List<Actuator_Action>();
            try
            {
                farmingContext.SensorRecords.Add(sensorData);
                farmingContext.SaveChanges();
                //
                var period = PeriodDetail(sensorData.Farm_ComponentId);
                if (sensorData.Temperature != null)
                    listAction.Add(ActionForTemp(period, (double)sensorData.Temperature));
                if (sensorData.Luminosity != null)
                    listAction.Add(ActionForLight(period, (double)sensorData.Luminosity));
                if (sensorData.Soil_Humidity != null)
                    listAction.Add(ActionForSoilHum(period, (double)sensorData.Soil_Humidity));
                return listAction;
            }
            catch
            {
                // actuator do nothing
                return listAction;
            }
        }

        /// <summary>
        /// Get Environment Data of current Farm Component
        /// </summary>
        /// <param name="farmComponentID"></param>
        /// <returns></returns>
        public List<Sensor_Record> GetSensorData(long farmComponentID)
        {
            return farmingContext.SensorRecords.Where(s => s.Farm_ComponentId == farmComponentID).ToList();
        }

        /// <summary>
        /// Get detailed information of a Period
        /// </summary>
        /// <param name="farmComponentId"></param>
        /// <returns></returns>
        private PeriodKB PeriodDetail(long farmComponentId)
        {
            var listPlant = farmingContext.Plants
                .Where(data => data.Farm_ComponentId == farmComponentId)
                .OrderBy(data => data.StartPlantDate)
                .ToList();
            var plant = listPlant[0];
            return farmingContext.Periods
                .Where(data => data.PlantKBId == plant.PlantKBId && data.Period == plant.CurPeriod)
                .SingleOrDefault();
        }

        /// <summary>
        /// Handle Action of Fan with Temperature Property
        /// </summary>
        /// <param name="period"></param>
        /// <param name="temp"></param>
        /// <returns></returns>
        private Actuator_Action ActionForTemp(PeriodKB period, double temp)
        {
            if(temp > period.Temp_Max)
            {
                return new Actuator_Action
                {
                    ActuatorType = ActuatorType.Fan,
                    ActuatorStatus = Acutator_Status.Open
                };
            }
            return new Actuator_Action
            {
                ActuatorType = ActuatorType.Fan,
                ActuatorStatus = Acutator_Status.Close
            };
        }

        /// <summary>
        /// Handle Action of Lamp with Light property
        /// </summary>
        /// <param name="period"></param>
        /// <param name="luminosity"></param>
        /// <returns></returns>
        private Actuator_Action ActionForLight(PeriodKB period, double luminosity)
        {
            if(luminosity < period.Luminosity_Min)
            {
                return new Actuator_Action
                {
                    ActuatorType = ActuatorType.Lamp,
                    ActuatorStatus = Acutator_Status.Open
                };
            }
            return new Actuator_Action
            {
                ActuatorType = ActuatorType.Lamp,
                ActuatorStatus = Acutator_Status.Close
            };
        }

        /// <summary>
        /// Hand Action of Pump with Soil_Humidity Property
        /// </summary>
        /// <param name="period"></param>
        /// <param name="soil_hum"></param>
        /// <returns></returns>
        private Actuator_Action ActionForSoilHum(PeriodKB period, double soil_hum)
        {
            if(soil_hum < period.Soil_Hum_Min)
            {
                return new Actuator_Action
                {
                    ActuatorType = ActuatorType.Pump,
                    ActuatorStatus = Acutator_Status.Open
                };
            }
            return new Actuator_Action
            {
                ActuatorType = ActuatorType.Pump,
                ActuatorStatus = Acutator_Status.Close
            };
        }
    }
}
