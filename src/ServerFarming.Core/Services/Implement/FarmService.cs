using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmingDatabase.Model;
using ServerFarming.Core.Model;
using ServerFarming.Core.Command;
using ServerFarming.Core.Repositories;
using FarmingDatabase.DatabaseContext;
using ServerFarming.Core.Exceptions;

namespace ServerFarming.Core.Services.Implement
{
    /// <summary>
    /// FarmService use for handling data related a Far before saving into Database
    /// </summary>
    public class FarmService : IFarmService
    {
        private readonly IFarmRepository farmRepository;
        private readonly IPlantRepository plantRepository;
        private readonly IAuthenticationService authenticationService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="farmRepository">used by DI</param>
        /// <param name="plantRepository">used by DI</param>
        /// <param name="authenticationService">used by DI</param>
        public FarmService(IFarmRepository farmRepository, 
            IPlantRepository plantRepository,
            IAuthenticationService authenticationService)
        {
            this.farmRepository = farmRepository;
            this.plantRepository = plantRepository;
            this.authenticationService = authenticationService;
        }

        /// <summary>
        /// Add New Farm
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="farmCommand"></param>
        /// <returns></returns>
        public async Task<Farm> AddFarm(long userId, FarmCommand farmCommand)
        {
            var newFarm = CopyFromFarm(userId, farmCommand);
            await farmRepository.AddNewFarm(newFarm);
            return newFarm;
        }

        /// <summary>
        /// Add New Farm Component
        /// </summary>
        /// <param name="farmComponentDTO"></param>
        /// <returns></returns>
        public async Task<Farm_Component> AddFarmComponent(FarmingComponentDTO farmComponentDTO)
        {
            var newFarmComponent = CopyFromFarmComponent(farmComponentDTO);
            await farmRepository.AddNewFarmComponent(newFarmComponent);
            return newFarmComponent;
        }

        /// <summary>
        /// Casr FarmingComponent Data to Object for saving in database
        /// </summary>
        /// <param name="farmComponentDTO"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Cast Farm Command to Farm Data for saving in database
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="farmCommand"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get all farm of user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Farm>> GetUserFarms(long userId)
        {
            return await farmRepository.GetFarmByUserID(userId);
        }

        /// <summary>
        /// Get all farm components in a farm
        /// </summary>
        /// <param name="farmID"></param>
        /// <returns></returns>
        public async Task<List<Farm_Component>> GetFarmComponents(long farmID)
        {
            return await farmRepository.GetFarmComponents(farmID);
        }

        /// <summary>
        /// Get Environment Data TODAY of system
        /// </summary>
        /// <param name="farmComponentId"></param>
        /// <returns></returns>
        public async Task<List<Sensor_Record>> GetEnvInfoToday(long farmComponentId)
        {
            var userId = authenticationService.GetUserId();
            var isOwned = plantRepository.CheckFarmComponentWithUserId(userId, farmComponentId);
            if (isOwned)
            {
                return await farmRepository.GetEnvInfoToday(farmComponentId);
            }
            else
            {
                throw new DataAccessException("Cannot Access Data This Farm Component!");
            }
        }

        /// <summary>
        /// Get Environment Data Latest sent
        /// </summary>
        /// <param name="farmComponentId"></param>
        /// <returns></returns>
        public Task<Sensor_Record> GetEnvInfoLatest(long farmComponentId)
        {
            var userId = authenticationService.GetUserId();
            var isOwned = plantRepository.CheckFarmComponentWithUserId(userId, farmComponentId);
            if (isOwned)
            {
                return farmRepository.GetEnvInfoLatest(farmComponentId);
            }
            else
            {
                throw new DataAccessException("Cannot Access Data This Farm Component!");
            }
        }

        /// <summary>
        /// Get Environment Data with a choosen Date
        /// </summary>
        /// <param name="farmComponentId"></param>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public async Task<List<Sensor_Record>> GetEnvInfoWithDate(long farmComponentId, int day, int month, int year)
        {
            var userId = authenticationService.GetUserId();
            var isOwned = plantRepository.CheckFarmComponentWithUserId(userId, farmComponentId);
            if (isOwned)
            {
                return await farmRepository.GetEnvInfoWithDate(farmComponentId, day, month, year);
            }
            else
            {
                throw new DataAccessException("Cannot Access Data This Farm Component!");
            }
        }
    }
}
