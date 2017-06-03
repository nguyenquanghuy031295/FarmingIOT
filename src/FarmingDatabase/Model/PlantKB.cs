using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FarmingDatabase.Model
{
    public class PlantKB
    {
        [Key]
        public long PlantKBId { get; set; }
        [StringLength(maximumLength: 20, ErrorMessage = "Name Max Length: 20 characters")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Kind { get; set; }
        public ICollection<PeriodKB> Period { get; set; }
        public virtual PlantType PlantDB { get; set; }
    }
}
