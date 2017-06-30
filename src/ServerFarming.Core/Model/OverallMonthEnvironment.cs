using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFarming.Core.Model
{
    public class OverallMonthEnvironment
    {
        public Nullable<double> Temperature { get; set; }
        public Nullable<double> Luminosity { get; set; }
        public Nullable<double> Soil_Humidity { get; set; }
    }
}
