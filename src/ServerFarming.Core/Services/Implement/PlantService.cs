using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmingDatabase.Model;
using ServerFarming.Core.Command;
using ServerFarming.Core.Model;
using ServerFarming.Core.Repositories;
using ServerFarming.Core.Exceptions;

namespace ServerFarming.Core.Services.Implement
{
    /// <summary>
    /// PlantService used for handing data related to a Plant before saving into Database
    /// </summary>
    public class PlantService : IPlantService
    {
        private readonly IPlantRepository plantRepository;
        private readonly IAuthenticationService authenticationService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="plantRepository">used by DI</param>
        /// <param name="authenticationService">used by DI</param>
        public PlantService(IPlantRepository plantRepository,
            IAuthenticationService authenticationService)
        {
            this.plantRepository = plantRepository;
            this.authenticationService = authenticationService;
        }

        /// <summary>
        /// Add a Plant in a Farm Component
        /// </summary>
        /// <param name="farmingComponentDTO"></param>
        /// <param name="farmComponentId"></param>
        /// <returns></returns>
        public async Task<PlantType> AddPlant(FarmingComponentDTO farmingComponentDTO, long farmComponentId)
        {
            var plant = CopyFrom(farmingComponentDTO, farmComponentId);
            await plantRepository.AddNewPlant(plant);
            return plant;
        }

        /// <summary>
        /// Get All Plant exist in Database (NOT USE)
        /// </summary>
        /// <returns></returns>
        public List<PlantKB> GetAllPlant()
        {
            return plantRepository.GetAllPlant();
        }


        /// <summary>
        /// Get detail of a Plant
        /// </summary>
        /// <param name="farmComponentId"></param>
        /// <returns></returns>
        public List<PlantDetail> GetPlantDetail(long farmComponentId)
        {
            return plantRepository.GetPlantDetail(farmComponentId);
        }

        /// <summary>
        /// Copy data of a plant in Farm Component Command -> Plant Object
        /// </summary>
        /// <param name="farmingComponentDTO"></param>
        /// <param name="farmComponentId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Ask System with current Farm Component, could user change current Period?
        /// </summary>
        /// <param name="farmComponentId"></param>
        /// <returns></returns>
        public ChangePeriodSignal AskChangePeriod(long farmComponentId)
        {
            try
            {
                var isLastPeriod = plantRepository.IsLastPeriod(farmComponentId);
                if (isLastPeriod)
                {
                    return new ChangePeriodSignal
                    {
                        Signal = SignalPeriod.IsLastPeroid
                    };
                }
                var isEnoughDay = plantRepository.IsEnoughDayToChangePeriod(farmComponentId);
                if (isEnoughDay)
                {
                    return new ChangePeriodSignal
                    {
                        Signal = SignalPeriod.IsAvailable
                    };
                }
                else
                {
                    return new ChangePeriodSignal
                    {
                        Signal = SignalPeriod.IsNotEnoughDay
                    };
                }
            }
            catch(ChangePeriodException e)
            {
                return new ChangePeriodSignal
                {
                    Signal = SignalPeriod.IsNotAvailable
                };
            }
        }

        /// <summary>
        /// Get detailed information of next Period in current Farm Component
        /// </summary>
        /// <param name="farmComponentId"></param>
        /// <returns></returns>
        public PeriodDetail GetNextPeriodDetail(long farmComponentId)
        {
            var userId = authenticationService.GetUserId();
            var isOwned = plantRepository.CheckFarmComponentWithUserId(userId, farmComponentId);
            if (isOwned)
            {
                return plantRepository.GetNextPeriod(farmComponentId);
            }
            else
            {
                throw new UnAuthorizedException("This Farm Component is not yours!");
            }
        }

        /// <summary>
        /// Change Period of current Farm Component to next Period
        /// </summary>
        /// <param name="farmComponentId"></param>
        /// <returns></returns>
        public async Task ChangeNextPeriod(long farmComponentId)
        {
            await plantRepository.ChangeNextPeriod(farmComponentId);
        }

        /// <summary>
        /// Check User is owing current Farm Component or not?
        /// </summary>
        /// <param name="farmComponentId"></param>
        /// <returns></returns>
        public bool CheckUserHaveFarmComponent(long farmComponentId)
        {
            var userId = authenticationService.GetUserId();
            return plantRepository.CheckFarmComponentWithUserId(userId,farmComponentId);
        }
    }
}
