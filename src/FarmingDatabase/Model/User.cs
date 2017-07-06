using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmingDatabase.Model
{
    public class User
    {
        public User()
        {
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long UserId { get; set; }
        [StringLength(maximumLength: 20, MinimumLength = 6, ErrorMessage = "Name Length: 6-20 characters")]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Date)]
        public Nullable<DateTime> DOB { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }

        public ICollection<Farm> Farms { get; set; }
    }
}
