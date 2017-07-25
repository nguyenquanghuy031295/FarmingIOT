using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmingDatabase.Model;
using FarmingDatabase.DatabaseContext;
using ServerFarming.Core.Model;
using ServerFarming.Core.Command;

namespace ServerFarming.Core.Repositories.Implement
{
    public class PlantRepository : IPlantRepository
    {
        private readonly FarmingDbContext _farmingContext;
        public PlantRepository(FarmingDbContext farmingContext)
        {
            this._farmingContext = farmingContext;
        }
        async Task IPlantRepository.AddNewPlant(PlantType plant)
        {
            _farmingContext.Plants.Add(plant);
            await _farmingContext.SaveChangesAsync();
        }

        public List<PlantKB> GetAllPlant()
        {
            var result = _farmingContext.PlantsKB.ToList();
            return result;
        }

        public List<PlantDetail> GetPlantDetail(long farmComponentId)
        {
            List<PlantDetail> result = new List<PlantDetail>();
            var plants = _farmingContext.Plants.Where(plant => plant.Farm_ComponentId == farmComponentId).ToArray();
            foreach(PlantType plant in plants)
            {
                var currentPeriod = _farmingContext.Periods.Where(period => period.Period == plant.CurPeriod).FirstOrDefault();
                var plantInfo = _farmingContext.PlantsKB.Where(plantKB => plantKB.PlantKBId == plant.PlantKBId).FirstOrDefault();
                result.Add(new PlantDetail
                {
                    StartPlantDate = plant.StartPlantDate,
                    EndPlantDate = plant.EndPlantDate,
                    StartDayCurrentPeriod = plant.StartDayCurPeriod,
                    CurrentPerriodName = currentPeriod.Name,
                    CurrentPerriodDescription = currentPeriod.Description,
                    PlantName = plantInfo.Name,
                    PlantKind = plantInfo.Kind,
                    PlantDescrition = plantInfo.Description
                });
            }
            return result;
        }

        public ChangePeriodSignal IsLastPeriod(long farmComponentId)
        {
            var plant = _farmingContext.Plants.Where(x => x.Farm_ComponentId == farmComponentId).FirstOrDefault();
            var curPeriod = plant.CurPeriod;
            var plantId = plant.PlantKBId;
            var lastPeriod = _farmingContext.Periods
                .Where(x => x.PlantKBId == plantId)
                .OrderByDescending(x => x.Period)
                .First();
            if(curPeriod == lastPeriod.Period)
            {
                return new ChangePeriodSignal
                {
                    Signal = false
                };
            }
            else
            {
                return new ChangePeriodSignal
                {
                    Signal = true
                };
            }
        }
    }
}
