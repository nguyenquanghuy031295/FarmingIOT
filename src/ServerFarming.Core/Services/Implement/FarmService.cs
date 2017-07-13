using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmingDatabase.Model;
using ServerFarming.Core.Model;
using ServerFarming.Core.Repositories;
using FarmingDatabase.DatabaseContext;

namespace ServerFarming.Core.Services.Implement
{
    public class FarmService : IFarmService
    {
        private readonly IFarmRepository farmRepository;
        private readonly IPlantRepository plantRepository;
        public FarmService(IFarmRepository farmRepository, 
            IPlantRepository plantRepository)
        {
            this.farmRepository = farmRepository;
            this.plantRepository = plantRepository;
            
        }

        async Task<Farm> IFarmService.AddFarm(long userId, FarmCommand farmCommand)
        {
            var newFarm = CopyFromFarm(userId, farmCommand);
            await farmRepository.AddNewFarm(newFarm);
            return newFarm;
        }

        async Task<Farm_Component> IFarmService.AddFarmComponent(FarmingComponentDTO farmComponentDTO)
        {
            var newFarmComponent = CopyFromFarmComponent(farmComponentDTO);
            await farmRepository.AddNewFarmComponent(newFarmComponent);
            return newFarmComponent;
        }
        private Farm_Component CopyFromFarmComponent(FarmingComponentDTO farmComponentDTO)
        {
            Farm_Component newFarmComponent = new Farm_Component()
            {
                Name = farmComponentDTO.Name,
                Position_Lat = farmComponentDTO.Position_Lat,
                Position_Lng = farmComponentDTO.Position_Lng,
                FarmId = farmComponentDTO.FarmId
            };
            return newFarmComponent;
        }
        private Farm CopyFromFarm(long userId, FarmCommand farmCommand)
        {
            Farm newFarm = new Farm()
            {
                Name = farmCommand.Name,
                Address = farmCommand.Address,
                Boundary = farmCommand.Boundary,
                Position_Lat = farmCommand.Position_Lat,
                Position_Lng = farmCommand.Position_Lng,
                UserId = userId
            };
            return newFarm;
        }

        async Task<List<Farm>> IFarmService.GetUserFarms(long userId)
        {
            return await farmRepository.GetFarmByUserID(userId);
        }

        async Task<List<Farm_Component>> IFarmService.GetFarmComponents(long farmID)
        {
            return await farmRepository.GetFarmComponents(farmID);
        }

        public OverallMonthEnvironment GetOverallEnvironmentInfo(long farmComponentId)
        {
            return farmRepository.GetOverallEnvironmentInfo(farmComponentId);
        }

        async Task<List<Sensor_Record>> IFarmService.GetEnvInfoToday(long farmComponentId)
        {
            return await farmRepository.GetEnvInfoToday(farmComponentId);
        }

        public Task<Sensor_Record> GetEnvInfoLastest(long farmComponentId)
        {
            return farmRepository.GetEnvInfoLastest(farmComponentId);
        }
    }
}
