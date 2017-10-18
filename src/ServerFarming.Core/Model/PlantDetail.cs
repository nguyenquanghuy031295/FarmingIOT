using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFarming.Core.Model
{
    /// <summary>
    /// Model for Plant Detail
    /// </summary>
    public class PlantDetail
    {
        public DateTime StartPlantDate { get; set; }
        public Nullable<DateTime> EndPlantDate { get; set; }
        public string CurrentPerriodName { get; set; }
        public string CurrentPerriodDescription { get; set; }
        public int StartDayCurrentPeriod { get; set; }
        public string PlantName { get; set; }
        public string PlantKind { get; set; }
        public string PlantDescrition { get; set; }
    }
}
