using FarmingDatabase.Model;
using ServerFarming.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFarming.Core.Services
{
    public interface IPlantService
    {
        Task<PlantType> AddPlant(FarmingComponentDTO farmingComponentDTO, long farmComponentId);
        List<PlantDetail> GetPlantDetail(long farmComponentId);
        List<PlantKB> GetAllPlant();
    }
}
