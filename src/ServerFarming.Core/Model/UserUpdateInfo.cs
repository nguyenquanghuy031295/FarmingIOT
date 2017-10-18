using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFarming.Core.Model
{
    /// <summary>
    /// Model when user update his/her information
    /// </summary>
    public class UserUpdateInfo
    {
        public string Name { get; set; }
        public Nullable<DateTime> DOB { get; set; }
        public string Address { get; set; }
    }
}
