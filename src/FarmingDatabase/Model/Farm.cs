using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmingDatabase.Model
{
    public class Farm
    {
        public Farm()
        {
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long FarmId { get; set; }
        public string Address { get; set; }
        [StringLength(maximumLength: 20, MinimumLength = 6, ErrorMessage = "Name Length: 6-20 characters")]
        public string Name { get; set; }
        public Nullable<double> Position_Lat { get; set; }
        public Nullable<double> Position_Lng { get; set; }
        public string Boundary { get; set; }
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public ICollection<Farm_Component> Farm_Component { get; set; }
    }
}
