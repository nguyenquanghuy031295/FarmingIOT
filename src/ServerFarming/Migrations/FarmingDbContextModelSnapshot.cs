using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FarmingDatabase.DatabaseContext;
using FarmingDatabase.Model;

namespace ServerFarming.Migrations
{
    [DbContext(typeof(FarmingDbContext))]
    partial class FarmingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FarmingDatabase.Model.Actuator_Record", b =>
                {
                    b.Property<DateTime>("StartTime");

                    b.Property<int>("Type");

                    b.Property<long>("Farm_ComponentId");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("EndTime");

                    b.HasKey("StartTime", "Type", "Farm_ComponentId");

                    b.HasIndex("Farm_ComponentId");

                    b.ToTable("ActuatorRecords");
                });

            modelBuilder.Entity("FarmingDatabase.Model.Farm", b =>
                {
                    b.Property<long>("FarmId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Boundary");

                    b.Property<string>("Name")
                        .HasMaxLength(20);

                    b.Property<double?>("Position_Lat");

                    b.Property<double?>("Position_Lng");

                    b.Property<long>("UserId");

                    b.HasKey("FarmId");

                    b.HasIndex("UserId");

                    b.ToTable("Farms");
                });

            modelBuilder.Entity("FarmingDatabase.Model.Farm_Component", b =>
                {
                    b.Property<long>("Farm_ComponentId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("FarmId");

                    b.Property<string>("Name")
                        .HasMaxLength(20);

                    b.Property<double?>("Position_Lat");

                    b.Property<double?>("Position_Lng");

                    b.HasKey("Farm_ComponentId");

                    b.HasIndex("FarmId");

                    b.ToTable("FarmComponents");
                });

            modelBuilder.Entity("FarmingDatabase.Model.Farm_Log", b =>
                {
                    b.Property<DateTime>("Date_Time");

                    b.Property<string>("Description");

                    b.Property<long>("Farm_ComponentId");

                    b.HasKey("Date_Time");

                    b.HasIndex("Farm_ComponentId");

                    b.ToTable("FarmLogs");
                });

            modelBuilder.Entity("FarmingDatabase.Model.Period", b =>
                {
                    b.Property<long>("PeriodId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("AirHumidity_Max");

                    b.Property<double>("AirHumidity_Min");

                    b.Property<double?>("AirHumidity_Optimal");

                    b.Property<double>("GroundHumidity_Max");

                    b.Property<double>("GroundHumidity_Min");

                    b.Property<double?>("GroundHumidity_Optimal");

                    b.Property<double>("IntensityLight_Max");

                    b.Property<double>("IntensityLight_Min");

                    b.Property<double?>("IntensityLight_Optimal");

                    b.Property<string>("Name");

                    b.Property<double>("PH_Max");

                    b.Property<double>("PH_Min");

                    b.Property<double?>("PH_Optimal");

                    b.Property<int?>("Period_Time_EndDate1");

                    b.Property<int?>("Period_Time_EndDate2");

                    b.Property<int>("Period_Time_Index");

                    b.Property<int?>("Period_Time_StartDay1");

                    b.Property<int?>("Period_Time_StartDay2");

                    b.Property<long>("PlantKBId");

                    b.Property<double>("Temperature_Max");

                    b.Property<double>("Temperature_Min");

                    b.Property<double?>("Temperature_Optimal");

                    b.Property<double>("Wind_Max");

                    b.Property<double>("Wind_Min");

                    b.Property<double?>("Wind_Optimal");

                    b.HasKey("PeriodId");

                    b.HasIndex("PlantKBId");

                    b.ToTable("Periods");
                });

            modelBuilder.Entity("FarmingDatabase.Model.PlantKB", b =>
                {
                    b.Property<long>("PlantKBId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Kind");

                    b.Property<string>("Name")
                        .HasMaxLength(20);

                    b.HasKey("PlantKBId");

                    b.ToTable("PlantsKB");
                });

            modelBuilder.Entity("FarmingDatabase.Model.PlantType", b =>
                {
                    b.Property<DateTime>("StartPlantDate");

                    b.Property<DateTime?>("EndPlantDate");

                    b.Property<long>("Farm_ComponentId");

                    b.Property<string>("Name")
                        .HasMaxLength(20);

                    b.Property<long>("PlantKBId");

                    b.HasKey("StartPlantDate");

                    b.HasIndex("Farm_ComponentId");

                    b.HasIndex("PlantKBId")
                        .IsUnique();

                    b.ToTable("Plants");
                });

            modelBuilder.Entity("FarmingDatabase.Model.Sensor_Record", b =>
                {
                    b.Property<DateTime>("Timestamp");

                    b.Property<double?>("Air_Humidity");

                    b.Property<long>("Farm_ComponentId");

                    b.Property<double?>("Ground_Humidity");

                    b.Property<double?>("InsensityLight");

                    b.Property<double?>("Temperature");

                    b.Property<double?>("Wind");

                    b.Property<double?>("pH");

                    b.HasKey("Timestamp");

                    b.HasIndex("Farm_ComponentId");

                    b.ToTable("SensorRecords");
                });

            modelBuilder.Entity("FarmingDatabase.Model.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<DateTime?>("DOB");

                    b.Property<string>("Email");

                    b.Property<string>("Name")
                        .HasMaxLength(20);

                    b.Property<string>("Password");

                    b.Property<string>("Role");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FarmingDatabase.Model.Actuator_Record", b =>
                {
                    b.HasOne("FarmingDatabase.Model.Farm_Component", "Farm_Component")
                        .WithMany("Actuator_Record")
                        .HasForeignKey("Farm_ComponentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FarmingDatabase.Model.Farm", b =>
                {
                    b.HasOne("FarmingDatabase.Model.User", "User")
                        .WithMany("Farms")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FarmingDatabase.Model.Farm_Component", b =>
                {
                    b.HasOne("FarmingDatabase.Model.Farm", "Farm")
                        .WithMany("Farm_Component")
                        .HasForeignKey("FarmId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FarmingDatabase.Model.Farm_Log", b =>
                {
                    b.HasOne("FarmingDatabase.Model.Farm_Component", "Farm_Component")
                        .WithMany("Farm_Log")
                        .HasForeignKey("Farm_ComponentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FarmingDatabase.Model.Period", b =>
                {
                    b.HasOne("FarmingDatabase.Model.PlantKB", "PlantKB")
                        .WithMany("Period")
                        .HasForeignKey("PlantKBId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FarmingDatabase.Model.PlantType", b =>
                {
                    b.HasOne("FarmingDatabase.Model.Farm_Component", "Farm_Component")
                        .WithMany("Plant")
                        .HasForeignKey("Farm_ComponentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FarmingDatabase.Model.PlantKB", "PlantKB")
                        .WithOne("PlantDB")
                        .HasForeignKey("FarmingDatabase.Model.PlantType", "PlantKBId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FarmingDatabase.Model.Sensor_Record", b =>
                {
                    b.HasOne("FarmingDatabase.Model.Farm_Component", "Farm_Component")
                        .WithMany("Sensor_Record")
                        .HasForeignKey("Farm_ComponentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
