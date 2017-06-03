using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FarmingDatabase.Model
{
    public class PeriodKB
    {
        public long PlantKBId { get; set; }
        public int Period { get; set; }
        public string Name { get; set; }
        public int Duration_Max { get; set; }
        public int Duration_Min { get; set; }
        public double Temp_Max { get; set; }
        public Nullable<double> Temp_Opt { get; set; }
        public double Temp_Min { get; set; }
        public double Soil_Hum_Max { get; set; }
        public Nullable<double> Soil_Hum_Opt { get; set; }
        public double Soil_Hum_Min { get; set; }
        public double Air_Hum_Max { get; set; }
        public Nullable<double> Air_Hum_Opt { get; set; }
        public double Air_Hum_Min { get; set; }
        public double Luminosity_Max { get; set; }
        public Nullable<double> Luminosity_Opt { get; set; }
        public double Luminosity_Min { get; set; }
        public double pH_Max { get; set; }
        public Nullable<double> pH_Opt { get; set; }
        public double pH_Min { get; set; }
        public double Wind_Max { get; set; }
        public Nullable<double> Wind_Opt { get; set; }
        public double Wind_Min { get; set; }

        [ForeignKey("PlantKBId")]
        public PlantKB PlantKB { get; set; }

        public string Description { get; set; }

        public virtual ICollection<PlantType> Plants { get; set; }
    }
}