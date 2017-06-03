using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmingDatabase.Model;
using FarmingDatabase.DatabaseContext;
using ServerFarming.Core.Model;

namespace ServerFarming.Core.Repositories.Implement
{
    public class PlantRepository : IPlantRepository
    {
        private readonly FarmingDbContext _farmingContext;
        public PlantRepository(FarmingDbContext farmingContext)
        {
            this._farmingContext = farmingContext;
        }
        public void AddNewPlant(PlantType plant)
        {
            _farmingContext.Plants.Add(plant);
            _farmingContext.SaveChanges();
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
    }
}
