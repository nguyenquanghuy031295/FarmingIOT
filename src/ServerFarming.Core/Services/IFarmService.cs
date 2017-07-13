using FarmingDatabase.Model;
using ServerFarming.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFarming.Core.Services
{
    public interface IFarmService
    {
        Task<Farm_Component> AddFarmComponent(FarmingComponentDTO farmComponentDTO);
        Task<Farm> AddFarm(long userId, FarmCommand farmCommand);
        Task<List<Farm>> GetUserFarms(long userId);
        Task<List<Farm_Component>> GetFarmComponents(long userID);
        OverallMonthEnvironment GetOverallEnvironmentInfo(long farmComponentId);
        Task<List<Sensor_Record>> GetEnvInfoToday(long farmComponentId);
        Task<Sensor_Record> GetEnvInfoLastest(long farmComponentId);
    }
}
