using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FarmingDatabase.Model;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FarmingDatabase.DatabaseContext
{
    public class FarmingDbContext :
        IdentityDbContext<IdentityUser<long>, IdentityRole<long>, long>,
        IFarmingDbContext
    {
        public FarmingDbContext(DbContextOptions<FarmingDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Actuator_Record>().HasKey(e => new { e.StartTime, e.Type, e.Farm_ComponentId});
            modelBuilder.Entity<PeriodKB>().HasKey(e => new { e.PlantKBId, e.Period});
            modelBuilder.Entity<PlantType>().HasKey(k => new { k.StartPlantDate, k.Farm_ComponentId});
            modelBuilder.Entity<PlantType>()
                .HasOne(p => p.PeriodKB)
                .WithMany(f => f.Plants)
                .IsRequired()
                .HasForeignKey(f => new { f.PlantKBId, f.CurPeriod })
                .OnDelete(DeleteBehavior.Restrict);
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Farm> Farms { get; set; }
        public DbSet<Farm_Component> FarmComponents { get; set; }
        public DbSet<Farm_Log> FarmLogs { get; set; }
        public DbSet<Sensor_Record> SensorRecords { get; set; }
        public DbSet<Actuator_Record> ActuatorRecords { get; set; }
        public DbSet<PlantType> Plants { get; set; }
        public DbSet<PlantKB> PlantsKB { get; set; }
        public DbSet<PeriodKB> Periods { get; set; }
    }
}
