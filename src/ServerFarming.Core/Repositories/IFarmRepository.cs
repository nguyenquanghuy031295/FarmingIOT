using FarmingDatabase.Model;
using ServerFarming.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFarming.Core.Repositories
{
    public interface IFarmRepository
    {
        Task AddNewFarm(Farm farm);
        Task AddNewFarmComponent(Farm_Component farmComponent);
        Task<List<Farm>> GetFarmByUserID(long userID);
        Task<List<Farm_Component>> GetFarmComponents(long farmID);
        OverallMonthEnvironment GetOverallEnvironmentInfo(long farmComponentId);
        Task<List<Sensor_Record>> GetEnvInfoToday(long farmComponentId);
        Task<Sensor_Record> GetEnvInfoLastest(long farmComponentId);
        Task<List<Sensor_Record>> GetEnvInfoWithDate(int day, int month, int year);
    }
}
