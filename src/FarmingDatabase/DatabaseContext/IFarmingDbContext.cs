using FarmingDatabase.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmingDatabase.DatabaseContext
{
    internal interface IFarmingDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Farm> Farms { get; set; }
        DbSet<Farm_Component> FarmComponents { get; set; }
        DbSet<Farm_Log> FarmLogs { get; set; }
        DbSet<Sensor_Record> SensorRecords { get; set; }
        DbSet<Actuator_Record> ActuatorRecords { get; set; }
        DbSet<PlantType> Plants { get; set; }
        DbSet<PlantKB> PlantsKB { get; set; }
        DbSet<PeriodKB> Periods { get; set; }
    }
}
