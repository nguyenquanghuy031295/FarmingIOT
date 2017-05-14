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
        public PlantType AddPlant(FarmingComponentDTO farmingComponentDTO, long farmComponentId)
        {
            var plant = CopyFrom(farmingComponentDTO);
            plant.Farm_ComponentId = farmComponentId;///
            plantRepository.AddNewPlant(plant);
            return plant;
        }
        private PlantType CopyFrom(FarmingComponentDTO farmingComponentDTO)
        {
            PlantType Plant = new PlantType()
            {
                PlantKBId = farmingComponentDTO.PlantKBId,
                Name = farmingComponentDTO.Plant_Name,
                StartPlantDate = farmingComponentDTO.StartPlantDate,
                EndPlantDate = farmingComponentDTO.EndPlantDate
            };
            return Plant;
        }
    }
}
