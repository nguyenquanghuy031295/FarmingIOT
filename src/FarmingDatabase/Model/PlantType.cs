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
        [StringLength(maximumLength: 20, ErrorMessage = "Name Max Length: 20 characters")]
        public string Name { get; set; }
        [Key]
        public DateTime StartPlantDate { get; set; }
        public Nullable<DateTime> EndPlantDate { get; set; }
        public long Farm_ComponentId { get; set; }
        [ForeignKey("Farm_ComponentId")]
        public Farm_Component Farm_Component { get; set; }
        public long PlantKBId { get; set; }
        [ForeignKey("PlantKBId")]
        public virtual PlantKB PlantKB { get; set; }
    }
}
