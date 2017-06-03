using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFarming.Core.Model
{
    public class FarmingComponentDTO
    {
        [StringLength(maximumLength: 20, MinimumLength = 6, ErrorMessage = "Name Length: 6-20 characters")]
        public string FarmComponentName { get; set; }
        public Nullable<double> Position_Lat { get; set; }
        public Nullable<double> Position_Lng { get; set; }
        public long FarmId { get; set; }
        public DateTime StartPlantDate { get; set; }
        public Nullable<DateTime> EndPlantDate { get; set; }
        public long PlantKBId { get; set; }
    }
}
