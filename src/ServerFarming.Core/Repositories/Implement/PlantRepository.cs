using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmingDatabase.Model;
using FarmingDatabase.DatabaseContext;
using ServerFarming.Core.Model;
using ServerFarming.Core.Command;
using ServerFarming.Core.Exceptions;

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

        public Boolean IsLastPeriod(long farmComponentId)
        {
            var plant = _farmingContext.Plants.Where(x => x.Farm_ComponentId == farmComponentId).FirstOrDefault();
            if (plant == null)
                throw new ChangePeriodException("No Plant in This Farm Component");
            var curPeriod = plant.CurPeriod;
            var plantId = plant.PlantKBId;
            var lastPeriod = _farmingContext.Periods
                .Where(x => x.PlantKBId == plantId)
                .OrderByDescending(x => x.Period)
                .First();
            if(curPeriod == lastPeriod.Period)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public PeriodDetail GetNextPeriod(long farmComponentId)
        {
            var plant = _farmingContext.Plants.Where(x => x.Farm_ComponentId == farmComponentId).FirstOrDefault();
            if (plant == null)
                throw new ChangePeriodException("No Plant in This Farm Component");
            var curPeriod = plant.CurPeriod;
            var plantId = plant.PlantKBId;
            var nextPeriod = _farmingContext.Periods
                .Where(x => x.PlantKBId == plantId && x.Period == curPeriod + 1)
                .First();
            var periodDetail = (PeriodDetail)nextPeriod;
            return periodDetail;
        }

        public bool IsEnoughDayToChangePeriod(long farmComponentId)
        {
            var plant = _farmingContext.Plants.Where(x => x.Farm_ComponentId == farmComponentId).FirstOrDefault();
            if (plant == null)
                throw new ChangePeriodException("No Plant in This Farm Component");
            var today = DateTime.Now;
            var dayPassedInPeriod = (int)(today - plant.StartPlantDate).TotalDays - plant.StartDayCurPeriod;
            var curPeriod = _farmingContext.Periods
                .Where(x => x.Period == plant.CurPeriod)
                .First();
            if (dayPassedInPeriod < curPeriod.Duration_Min)
                return false;
            return true;
        }

        async Task IPlantRepository.ChangeNextPeriod(long farmComponentId)
        {
            var today = DateTime.Now;
            var plant = _farmingContext.Plants
                .Where(x => x.Farm_ComponentId == farmComponentId)
                .OrderByDescending(x => x.StartPlantDate)
                .First();
            var dayPassedInPeriod = (int)(today - plant.StartPlantDate).TotalDays - plant.StartDayCurPeriod;
            plant.CurPeriod += 1;
            plant.StartDayCurPeriod += dayPassedInPeriod;
            _farmingContext.Entry(plant).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _farmingContext.SaveChangesAsync();
        }

        public bool CheckFarmComponentWithUserId(long userId, long farmComponentId)
        {
            var farms = _farmingContext.Farms.Where(x => x.UserId == userId);
            var farmComponent = _farmingContext.FarmComponents.Where(x => x.Farm_ComponentId == farmComponentId);
            if (farmComponent.Count() > 0)
                return farms.Where(x => x.FarmId == farmComponent.First().FarmId).Count() > 0;
            else
                return false;
        }
    }
}
