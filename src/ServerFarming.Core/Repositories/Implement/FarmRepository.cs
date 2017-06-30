﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmingDatabase.Model;
using FarmingDatabase.DatabaseContext;

namespace ServerFarming.Core.Repositories.Implement
{
    public class FarmRepository : IFarmRepository
    {
        private readonly FarmingDbContext _farmingContext;
        public FarmRepository(FarmingDbContext farmingContext)
        {
            this._farmingContext = farmingContext;
        }
        public void AddNewFarm(Farm farm)
        {
            _farmingContext.Farms.Add(farm);
            _farmingContext.SaveChanges();
        }

        public void AddNewFarmComponent(Farm_Component farmComponent)
        {
            _farmingContext.FarmComponents.Add(farmComponent);
            _farmingContext.SaveChanges();
        }

        public List<Farm> GetFarmByUserID(long userID)
        {
            return _farmingContext.Farms.Where(farm => farm.UserId == userID).ToList();
        }

        public List<Farm_Component> GetFarmComponents(long farmID)
        {
            return _farmingContext.FarmComponents.Where(farmComponent => farmComponent.FarmId == farmID).ToList();
        }
    }
}