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
    public class PlantService : IPlantService
    {
        private readonly IPlantRepository plantRepository;
        private readonly IAuthenticationService authenticationService;
        public PlantService(IPlantRepository plantRepository,
            IAuthenticationService authenticationService)
        {
            this.plantRepository = plantRepository;
            this.authenticationService = authenticationService;
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

        async Task IPlantService.ChangeNextPeriod(long farmComponentId)
        {
            await plantRepository.ChangeNextPeriod(farmComponentId);
        }

        public bool CheckUserHaveFarmComponent(long farmComponentId)
        {
            var userId = authenticationService.GetUserId();
            return plantRepository.CheckFarmComponentWithUserId(userId,farmComponentId);
        }
    }
}
