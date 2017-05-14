using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmingDatabase.Model
{
    public class Sensor_Record
    {
        public Sensor_Record()
        {
        }
        [Key]
        public DateTime Timestamp { get; set; }
        public Nullable<double> pH { get; set; }
        public Nullable<double> Wind { get; set; }
        public Nullable<double> Temperature { get; set; }
        public Nullable<double> InsensityLight { get; set; }
        public Nullable<double> Air_Humidity { get; set; }
        public Nullable<double> Ground_Humidity { get; set; }
        public long Farm_ComponentId { get; set; }
        [ForeignKey("Farm_ComponentId")]
        public Farm_Component Farm_Component { get; set; }
    }
}
