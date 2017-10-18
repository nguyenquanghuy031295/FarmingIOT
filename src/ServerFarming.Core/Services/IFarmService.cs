using FarmingDatabase.Model;
using ServerFarming.Core.Model;
using ServerFarming.Core.Command;
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
        Task<List<Sensor_Record>> GetEnvInfoToday(long farmComponentId);
        Task<Sensor_Record> GetEnvInfoLatest(long farmComponentId);
        Task<List<Sensor_Record>> GetEnvInfoWithDate(long farmComponentId, int day, int month, int year);
    }
}
