using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FarmingDatabase.Model
{
    public class Period
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PeriodId { get; set; }
        public string Name { get; set; }
        public int Period_Time_Index { get; set; }
        public Nullable<int> Period_Time_StartDay1 { get; set; }
        public Nullable<int> Period_Time_StartDay2 { get; set; }
        public Nullable<int> Period_Time_EndDate1 { get; set; }
        public Nullable<int> Period_Time_EndDate2 { get; set; }
        public double Temperature_Max { get; set; }
        public Nullable<double> Temperature_Optimal { get; set; }
        public double Temperature_Min { get; set; }
        public double GroundHumidity_Max { get; set; }
        public Nullable<double> GroundHumidity_Optimal { get; set; }
        public double GroundHumidity_Min { get; set; }
        public double AirHumidity_Max { get; set; }
        public Nullable<double> AirHumidity_Optimal { get; set; }
        public double AirHumidity_Min { get; set; }
        public double IntensityLight_Max { get; set; }
        public Nullable<double> IntensityLight_Optimal { get; set; }
        public double IntensityLight_Min { get; set; }
        public double PH_Max { get; set; }
        public Nullable<double> PH_Optimal { get; set; }
        public double PH_Min { get; set; }
        public double Wind_Max { get; set; }
        public Nullable<double> Wind_Optimal { get; set; }
        public double Wind_Min { get; set; }
        public long PlantKBId { get; set; }
        [ForeignKey("PlantKBId")]
        public PlantKB PlantKB { get; set; }
    }
}