using FarmingDatabase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFarming.Core.Model
{
    /// <summary>
    /// Model for Period Detail
    /// </summary>
    public class PeriodDetail
    {
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
        public string Description { get; set; }

        public static explicit operator PeriodDetail(PeriodKB period)
        {
            PeriodDetail periodDetail = new PeriodDetail();
            periodDetail.Name = period.Name;

            periodDetail.Duration_Max = period.Duration_Max;
            periodDetail.Duration_Min = period.Duration_Min;

            periodDetail.Temp_Max = period.Temp_Max;
            periodDetail.Temp_Opt = period.Temp_Opt;
            periodDetail.Temp_Min = period.Temp_Min;

            periodDetail.Soil_Hum_Max = period.Soil_Hum_Max;
            periodDetail.Soil_Hum_Opt = period.Soil_Hum_Opt;
            periodDetail.Soil_Hum_Min = period.Soil_Hum_Min;

            periodDetail.Luminosity_Max = period.Luminosity_Max;
            periodDetail.Luminosity_Opt = period.Luminosity_Opt;
            periodDetail.Luminosity_Min = period.Luminosity_Min;

            periodDetail.Air_Hum_Max = period.Air_Hum_Max;
            periodDetail.Air_Hum_Opt = period.Air_Hum_Opt;
            periodDetail.Air_Hum_Min = period.Air_Hum_Min;

            periodDetail.pH_Max = period.pH_Max;
            periodDetail.pH_Opt = period.pH_Opt;
            periodDetail.pH_Min = period.pH_Min;

            periodDetail.Wind_Max = period.Wind_Max;
            periodDetail.Wind_Opt = period.Wind_Opt;
            periodDetail.Wind_Min = period.Wind_Min;

            periodDetail.Description = period.Description;
            return periodDetail;
        }
    }
}
