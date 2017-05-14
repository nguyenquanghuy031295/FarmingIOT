using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FarmingDatabase.Model;

namespace FarmingDatabase.DatabaseContext
{
    public class FarmingDbContext : DbContext
    {
        public FarmingDbContext(DbContextOptions<FarmingDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actuator_Record>().HasKey(e => new { e.StartTime, e.Type, e.Farm_ComponentId});
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Farm> Farms { get; set; }
        public DbSet<Farm_Component> FarmComponents { get; set; }
        public DbSet<Farm_Log> FarmLogs { get; set; }
        public DbSet<Sensor_Record> SensorRecords { get; set; }
        public DbSet<Actuator_Record> ActuatorRecords { get; set; }
        //
        public DbSet<PlantType> Plants { get; set; }
        public DbSet<PlantKB> PlantsKB { get; set; }
        public DbSet<Period> Periods { get; set; }
    }
}
