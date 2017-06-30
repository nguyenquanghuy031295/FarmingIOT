using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFarming.Core.Model
{
    public class UserUpdateInfo
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public Nullable<DateTime> DOB { get; set; }
        public string Address { get; set; }
    }
}
