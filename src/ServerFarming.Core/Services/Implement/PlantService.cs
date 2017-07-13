using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmingDatabase.Model;
using ServerFarming.Core.Model;
using ServerFarming.Core.Repositories;

namespace ServerFarming.Core.Services.Implement
{
    public class PlantService : IPlantService
    {
        private readonly IPlantRepository plantRepository;
        public PlantService(IPlantRepository plantRepository)
        {
            this.plantRepository = plantRepository;
        }
        async Task<PlantType> IPlantService.AddPlant(FarmingComponentDTO farmingComponentDTO, long farmComponentId)
        {
            var plant = CopyFrom(farmingComponentDTO, farmComponentId);
            await plantRepository.AddNewPlant(plant);
            return plant;
        }

        public List<PlantKB> GetAllPlant()
        {
            return plantRepository.GetAllPlant();
        }

        public List<PlantDetail> GetPlantDetail(long farmComponentId)
        {
            return plantRepository.GetPlantDetail(farmComponentId);
        }

        private PlantType CopyFrom(FarmingComponentDTO farmingComponentDTO, long farmComponentId)
        {
            PlantType Plant = new PlantType()
            {
                PlantKBId = farmingComponentDTO.PlantKBId,
                Farm_ComponentId = farmComponentId,
                StartPlantDate = farmingComponentDTO.StartPlantDate,
                EndPlantDate = farmingComponentDTO.EndPlantDate,
                CurPeriod = 1,
                StartDayCurPeriod = 0
            };
            return Plant;
        }
    }
}
