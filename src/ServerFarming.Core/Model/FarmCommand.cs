using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFarming.Core.Model
{
    public class FarmCommand
    {
        public string Address { get; set; }
        public string Name { get; set; }
        public Nullable<double> Position_Lat { get; set; }
        public Nullable<double> Position_Lng { get; set; }
        public string Boundary { get; set; }
    }
}
