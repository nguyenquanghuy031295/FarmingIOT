using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmingDatabase.Model
{
    public class Farm_Component
    {
        public Farm_Component()
        {
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Farm_ComponentId { get; set; }
        [StringLength(maximumLength: 20, MinimumLength = 6, ErrorMessage = "Name Length: 6-20 characters")]
        public string Name { get; set; }
        public Nullable<double> Position_Lat { get; set; }
        public Nullable<double> Position_Lng { get; set; }
        public long FarmId { get; set; }
        [ForeignKey("FarmId")]
        public Farm Farm { get; set; }
        public ICollection<Farm_Log> Farm_Log { get; set; }
        public ICollection<Sensor_Record> Sensor_Record { get; set; }
        public ICollection<Actuator_Record> Actuator_Record { get; set; }
        public ICollection<PlantType> Plant { get; set; }
    }
}
