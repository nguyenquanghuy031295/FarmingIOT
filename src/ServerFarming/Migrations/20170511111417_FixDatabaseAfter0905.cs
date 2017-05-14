using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ServerFarming.Migrations
{
    public partial class FixDatabaseAfter0905 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlantsKB",
                columns: table => new
                {
                    PlantKBId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Kind = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantsKB", x => x.PlantKBId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Periods",
                columns: table => new
                {
                    PeriodId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AirHumidity_Max = table.Column<double>(nullable: false),
                    AirHumidity_Min = table.Column<double>(nullable: false),
                    AirHumidity_Optimal = table.Column<double>(nullable: true),
                    GroundHumidity_Max = table.Column<double>(nullable: false),
                    GroundHumidity_Min = table.Column<double>(nullable: false),
                    GroundHumidity_Optimal = table.Column<double>(nullable: true),
                    IntensityLight_Max = table.Column<double>(nullable: false),
                    IntensityLight_Min = table.Column<double>(nullable: false),
                    IntensityLight_Optimal = table.Column<double>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PH_Max = table.Column<double>(nullable: false),
                    PH_Min = table.Column<double>(nullable: false),
                    PH_Optimal = table.Column<double>(nullable: true),
                    Period_Time_EndDate1 = table.Column<int>(nullable: true),
                    Period_Time_EndDate2 = table.Column<int>(nullable: true),
                    Period_Time_Index = table.Column<int>(nullable: false),
                    Period_Time_StartDay1 = table.Column<int>(nullable: true),
                    Period_Time_StartDay2 = table.Column<int>(nullable: true),
                    PlantKBId = table.Column<long>(nullable: false),
                    Temperature_Max = table.Column<double>(nullable: false),
                    Temperature_Min = table.Column<double>(nullable: false),
                    Temperature_Optimal = table.Column<double>(nullable: true),
                    Wind_Max = table.Column<double>(nullable: false),
                    Wind_Min = table.Column<double>(nullable: false),
                    Wind_Optimal = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periods", x => x.PeriodId);
                    table.ForeignKey(
                        name: "FK_Periods_PlantsKB_PlantKBId",
                        column: x => x.PlantKBId,
                        principalTable: "PlantsKB",
                        principalColumn: "PlantKBId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Farms",
                columns: table => new
                {
                    FarmId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    Boundary = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    Position_Lat = table.Column<double>(nullable: true),
                    Position_Lng = table.Column<double>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farms", x => x.FarmId);
                    table.ForeignKey(
                        name: "FK_Farms_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FarmComponents",
                columns: table => new
                {
                    Farm_ComponentId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FarmId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    Position_Lat = table.Column<double>(nullable: true),
                    Position_Lng = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmComponents", x => x.Farm_ComponentId);
                    table.ForeignKey(
                        name: "FK_FarmComponents_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "FarmId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActuatorRecords",
                columns: table => new
                {
                    StartTime = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Farm_ComponentId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActuatorRecords", x => new { x.StartTime, x.Type, x.Farm_ComponentId });
                    table.ForeignKey(
                        name: "FK_ActuatorRecords_FarmComponents_Farm_ComponentId",
                        column: x => x.Farm_ComponentId,
                        principalTable: "FarmComponents",
                        principalColumn: "Farm_ComponentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FarmLogs",
                columns: table => new
                {
                    Date_Time = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Farm_ComponentId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmLogs", x => x.Date_Time);
                    table.ForeignKey(
                        name: "FK_FarmLogs_FarmComponents_Farm_ComponentId",
                        column: x => x.Farm_ComponentId,
                        principalTable: "FarmComponents",
                        principalColumn: "Farm_ComponentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plants",
                columns: table => new
                {
                    StartPlantDate = table.Column<DateTime>(nullable: false),
                    EndPlantDate = table.Column<DateTime>(nullable: true),
                    Farm_ComponentId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    PlantKBId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.StartPlantDate);
                    table.ForeignKey(
                        name: "FK_Plants_FarmComponents_Farm_ComponentId",
                        column: x => x.Farm_ComponentId,
                        principalTable: "FarmComponents",
                        principalColumn: "Farm_ComponentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plants_PlantsKB_PlantKBId",
                        column: x => x.PlantKBId,
                        principalTable: "PlantsKB",
                        principalColumn: "PlantKBId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SensorRecords",
                columns: table => new
                {
                    Timestamp = table.Column<DateTime>(nullable: false),
                    Air_Humidity = table.Column<double>(nullable: true),
                    Farm_ComponentId = table.Column<long>(nullable: false),
                    Ground_Humidity = table.Column<double>(nullable: true),
                    InsensityLight = table.Column<double>(nullable: true),
                    Temperature = table.Column<double>(nullable: true),
                    Wind = table.Column<double>(nullable: true),
                    pH = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorRecords", x => x.Timestamp);
                    table.ForeignKey(
                        name: "FK_SensorRecords_FarmComponents_Farm_ComponentId",
                        column: x => x.Farm_ComponentId,
                        principalTable: "FarmComponents",
                        principalColumn: "Farm_ComponentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActuatorRecords_Farm_ComponentId",
                table: "ActuatorRecords",
                column: "Farm_ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Farms_UserId",
                table: "Farms",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmComponents_FarmId",
                table: "FarmComponents",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmLogs_Farm_ComponentId",
                table: "FarmLogs",
                column: "Farm_ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Periods_PlantKBId",
                table: "Periods",
                column: "PlantKBId");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_Farm_ComponentId",
                table: "Plants",
                column: "Farm_ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_PlantKBId",
                table: "Plants",
                column: "PlantKBId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SensorRecords_Farm_ComponentId",
                table: "SensorRecords",
                column: "Farm_ComponentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActuatorRecords");

            migrationBuilder.DropTable(
                name: "FarmLogs");

            migrationBuilder.DropTable(
                name: "Periods");

            migrationBuilder.DropTable(
                name: "Plants");

            migrationBuilder.DropTable(
                name: "SensorRecords");

            migrationBuilder.DropTable(
                name: "PlantsKB");

            migrationBuilder.DropTable(
                name: "FarmComponents");

            migrationBuilder.DropTable(
                name: "Farms");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
