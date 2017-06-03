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

        public Farm AddFarm(Farm farm)
        {
            var newFarm = CopyFromFarm(farm);
            farmRepository.AddNewFarm(newFarm);
            return newFarm;
        }

        public Farm_Component AddFarmComponent(FarmingComponentDTO farmComponentDTO)
        {
            var newFarmComponent = CopyFromFarmComponent(farmComponentDTO);
            farmRepository.AddNewFarmComponent(newFarmComponent);
            return newFarmComponent;
        }
        private Farm_Component CopyFromFarmComponent(FarmingComponentDTO farmComponentDTO)
        {
            Farm_Component newFarmComponent = new Farm_Component()
            {
                Name = farmComponentDTO.FarmComponentName,
                Position_Lat = farmComponentDTO.Position_Lat,
                Position_Lng = farmComponentDTO.Position_Lng,
                FarmId = farmComponentDTO.FarmId
            };
            return newFarmComponent;
        }
        private Farm CopyFromFarm(Farm farm)
        {
            Farm newFarm = new Farm()
            {
                Name = farm.Name,
                Address = farm.Address,
                Boundary = farm.Boundary,
                Position_Lat = farm.Position_Lat,
                Position_Lng = farm.Position_Lng,
                UserId = farm.UserId
            };
            return newFarm;
        }

        public List<Farm> GetFarmByUserID(long userID)
        {
            return farmRepository.GetFarmByUserID(userID);
        }

        public List<Farm_Component> GetFarmComponents(long farmID)
        {
            return farmRepository.GetFarmComponents(farmID);
        }
    }
}
