using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmingDatabase.Model;
using FarmingDatabase.DatabaseContext;

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
    }
}
