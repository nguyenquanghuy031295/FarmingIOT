using FarmingDatabase.Model;
using ServerFarming.Core.Command;
using ServerFarming.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFarming.Core.Repositories
{
    public interface IPlantRepository
    {
        Task AddNewPlant(PlantType plant);
        List<PlantDetail> GetPlantDetail(long farmComponentId);
        List<PlantKB> GetAllPlant();
        Boolean IsLastPeriod(long farmComponentId);
        PeriodDetail GetNextPeriod(long farmComponentId);
        Boolean IsEnoughDayToChangePeriod(long farmComponentId);
        Task ChangeNextPeriod(long farmComponentId);
        Boolean CheckFarmComponentWithUserId(long userId,long farmComponentId);
    }
}
