using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmingDatabase.Model
{
    public enum ActuatorType
    {
        Pump = 0,
        Lamp = 1
    }
    public class Actuator_Record
    {
        public DateTime StartTime { get; set; }
        public Nullable<DateTime> EndTime { get; set; }
        public ActuatorType Type { get; set; }
        public string Description { get; set; }
        public long Farm_ComponentId { get; set; }
        [ForeignKey("Farm_ComponentId")]
        public Farm_Component Farm_Component { get; set; }
    }
}
