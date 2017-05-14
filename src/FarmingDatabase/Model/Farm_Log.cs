using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmingDatabase.Model
{
    public class Farm_Log
    {
        public Farm_Log()
        {
            this.Date_Time = DateTime.Now;
        }
        [Key]
        public DateTime Date_Time { get; set; }
        public string Description { get; set; }
        public long Farm_ComponentId { get; set; }
        [ForeignKey("Farm_ComponentId")]
        public Farm_Component Farm_Component { get; set; }
    }
}
