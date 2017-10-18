using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmingDatabase.Model;
using FarmingDatabase.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using ServerFarming.Core.Model;
using System.Data.Common;
using ServerFarming.Core.Exceptions;

namespace ServerFarming.Core.Repositories.Implement
{
    /// <summary>
    /// Farm Repository use for savin data related to a farm into database
    /// </summary>
    public class FarmRepository : IFarmRepository
    {
        /// <summary>
        /// FarmingDbContext hold all tables we need to get data from it
        /// </summary>
        private readonly FarmingDbContext _farmingContext;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="farmingContext">used by DI</param>
        public FarmRepository(FarmingDbContext farmingContext)
        {
            this._farmingContext = farmingContext;
        }

        /// <summary>
        /// Add new Farm into Database
        /// </summary>
        /// <param name="farm"></param>
        /// <returns></returns>
        public async Task AddNewFarm(Farm farm)
        {
            _farmingContext.Farms.Add(farm);
            await _farmingContext.SaveChangesAsync();
        }

        /// <summary>
        /// Add New Farm Component into Database
        /// </summary>
        /// <param name="farmComponent"></param>
        /// <returns></returns>
        public async Task AddNewFarmComponent(Farm_Component farmComponent)
        {
            _farmingContext.FarmComponents.Add(farmComponent);
            await _farmingContext.SaveChangesAsync();
        }

        /// <summary>
        /// Get environment data TODAY of system
        /// </summary>
        /// <param name="farmComponentId"></param>
        /// <returns></returns>
        public async Task<List<Sensor_Record>> GetEnvInfoToday(long farmComponentId)
        {
            string dateFormat = "yyyy-MM-dd";
            var result = await _farmingContext.SensorRecords
                    .Where(sensorData => 
                        sensorData.Timestamp.ToString(dateFormat) == DateTime.Now.ToString(dateFormat)
                        && sensorData.Farm_ComponentId == farmComponentId)
                    .OrderByDescending(x => x.Timestamp)
                    .Take(10)
                    .OrderBy(x => x.Timestamp)
                    .ToListAsync();
            return result;
        }

        /// <summary>
        /// Get list farms owned of current users
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Task<List<Farm>> GetFarmByUserID(long userID)
        {
            return _farmingContext.Farms.Where(farm => farm.UserId == userID).ToListAsync();
        }

        /// <summary>
        /// Get list Farm Component in current Farm
        /// </summary>
        /// <param name="farmID"></param>
        /// <returns></returns>
        public Task<List<Farm_Component>> GetFarmComponents(long farmID)
        {
            return _farmingContext.FarmComponents.Where(farmComponent => farmComponent.FarmId == farmID).ToListAsync();
        }

        /// <summary>
        /// Get Environment Data Latest
        /// </summary>
        /// <param name="farmComponentId"></param>
        /// <returns></returns>
        public Task<Sensor_Record> GetEnvInfoLatest(long farmComponentId)
        {
            var sensorDatas = _farmingContext.SensorRecords.OrderByDescending(x => x.Timestamp);
            if (sensorDatas.Count() == 0)
                throw new NoDataException("No Sensor Data in Database");
            else
                return sensorDatas.FirstAsync();
        }

        /// <summary>
        /// Get Environment Data with the Date user choose
        /// </summary>
        /// <param name="farmComponentId"></param>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public async Task<List<Sensor_Record>> GetEnvInfoWithDate(long farmComponentId, int day, int month, int year)
        {
            var listEnvInfo =
                 _farmingContext
                    .SensorRecords
                    .Where(x => ( x.Timestamp.Day == day 
                        && x.Timestamp.Month == month 
                        && x.Timestamp.Year == year 
                        && x.Farm_ComponentId == farmComponentId))
                    .OrderByDescending(x => x.Timestamp)
                    .Take(10)
                    .OrderBy(x => x.Timestamp)
                    .ToListAsync();
            return await listEnvInfo;
        }
    }
}
