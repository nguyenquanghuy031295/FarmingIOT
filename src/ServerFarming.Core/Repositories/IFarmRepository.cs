﻿using FarmingDatabase.Model;
using ServerFarming.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFarming.Core.Repositories
{
    public interface IFarmRepository
    {
        void AddNewFarm(Farm farm);
        void AddNewFarmComponent(Farm_Component farmComponent);
        List<Farm> GetFarmByUserID(long userID);
        List<Farm_Component> GetFarmComponents(long farmID);
        OverallMonthEnvironment GetOverallEnvironmentInfo(long farmComponentId);
        List<Sensor_Record> GetEnvInfoToday(long farmComponentId);
    }
}
