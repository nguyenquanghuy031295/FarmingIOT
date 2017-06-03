using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmingDatabase.Model
{
    public class PlantType
    {
        public DateTime StartPlantDate { get; set; }
        public Nullable<DateTime> EndPlantDate { get; set; }
        public long Farm_ComponentId { get; set; }

        [ForeignKey("Farm_ComponentId")]
        public Farm_Component Farm_Component { get; set; }

        public long PlantKBId { get; set; }
        public int CurPeriod { get; set; }
        public int StartDayCurPeriod { get; set; }
        public virtual PeriodKB PeriodKB { get; set; }
    }
}
