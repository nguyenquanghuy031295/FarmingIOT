using FarmingDatabase.Model;
using ServerFarming.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFarming.Core.Services
{
    public interface IFarmService
    {
        Farm_Component AddFarmComponent(FarmingComponentDTO farmComponentDTO);
        Farm AddFarm(Farm farm);
        List<Farm> GetFarmByUserID(long userID);
        List<Farm_Component> GetFarmComponents(long userID);
    }
}
