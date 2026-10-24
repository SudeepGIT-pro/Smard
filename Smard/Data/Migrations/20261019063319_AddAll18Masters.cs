using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smard.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAll18Masters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AVSUMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrandTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaterElectricityPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToolsEquipmentPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfitPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProfitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalRateAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AVSUMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BHUMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrandTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaterElectricityPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToolsEquipmentPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfitPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProfitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalRateAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BHUMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CeilingSystemMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrandTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaterElectricityPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToolsEquipmentPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfitPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProfitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalRateAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CeilingSystemMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CountersMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrandTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaterElectricityPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToolsEquipmentPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfitPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProfitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalRateAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountersMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CSSDFurnituresMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrandTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaterElectricityPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToolsEquipmentPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfitPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProfitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalRateAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CSSDFurnituresMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoorsMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrandTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaterElectricityPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToolsEquipmentPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfitPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProfitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalRateAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoorsMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FixedFurnituresMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrandTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaterElectricityPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToolsEquipmentPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfitPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProfitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalRateAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixedFurnituresMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobWorksMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrandTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaterElectricityPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToolsEquipmentPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfitPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProfitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalRateAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobWorksMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LaminarFlowMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrandTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaterElectricityPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToolsEquipmentPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfitPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProfitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalRateAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaminarFlowMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LGSPanelSystemMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrandTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaterElectricityPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToolsEquipmentPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfitPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProfitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalRateAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LGSPanelSystemMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LooseFurnituresMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrandTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaterElectricityPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToolsEquipmentPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfitPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProfitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalRateAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LooseFurnituresMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PeripheralLightMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrandTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaterElectricityPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToolsEquipmentPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfitPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProfitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalRateAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeripheralLightMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReturnAirSystemMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrandTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaterElectricityPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToolsEquipmentPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfitPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProfitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalRateAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnAirSystemMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SinkMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrandTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaterElectricityPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToolsEquipmentPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfitPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProfitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalRateAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinkMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StorageCabinetMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrandTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaterElectricityPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToolsEquipmentPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfitPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProfitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalRateAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageCabinetMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WallPanelSystemMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrandTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaterElectricityPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToolsEquipmentPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfitPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProfitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalRateAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WallPanelSystemMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WindowsMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrandTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaterElectricityPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToolsEquipmentPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfitPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProfitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalRateAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WindowsMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkStationMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensionArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrandTotalPerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportationRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaterElectricityPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterElectricityRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToolsEquipmentPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToolsEquipmentRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfitPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProfitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalRateAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkStationMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AVSUCostDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AVSUMasterId = table.Column<int>(type: "int", nullable: false),
                    CostType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SlNo = table.Column<int>(type: "int", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AVSUCostDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AVSUCostDetails_AVSUMasters_AVSUMasterId",
                        column: x => x.AVSUMasterId,
                        principalTable: "AVSUMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BHUCostDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BHUMasterId = table.Column<int>(type: "int", nullable: false),
                    CostType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SlNo = table.Column<int>(type: "int", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BHUCostDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BHUCostDetails_BHUMasters_BHUMasterId",
                        column: x => x.BHUMasterId,
                        principalTable: "BHUMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CeilingSystemCostDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CeilingSystemMasterId = table.Column<int>(type: "int", nullable: false),
                    CostType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SlNo = table.Column<int>(type: "int", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CeilingSystemCostDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CeilingSystemCostDetails_CeilingSystemMasters_CeilingSystemMasterId",
                        column: x => x.CeilingSystemMasterId,
                        principalTable: "CeilingSystemMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountersCostDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountersMasterId = table.Column<int>(type: "int", nullable: false),
                    CostType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SlNo = table.Column<int>(type: "int", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountersCostDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountersCostDetails_CountersMasters_CountersMasterId",
                        column: x => x.CountersMasterId,
                        principalTable: "CountersMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CSSDFurnituresCostDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CSSDFurnituresMasterId = table.Column<int>(type: "int", nullable: false),
                    CostType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SlNo = table.Column<int>(type: "int", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CSSDFurnituresCostDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CSSDFurnituresCostDetails_CSSDFurnituresMasters_CSSDFurnituresMasterId",
                        column: x => x.CSSDFurnituresMasterId,
                        principalTable: "CSSDFurnituresMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoorsCostDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoorsMasterId = table.Column<int>(type: "int", nullable: false),
                    CostType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SlNo = table.Column<int>(type: "int", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoorsCostDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoorsCostDetails_DoorsMasters_DoorsMasterId",
                        column: x => x.DoorsMasterId,
                        principalTable: "DoorsMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FixedFurnituresCostDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FixedFurnituresMasterId = table.Column<int>(type: "int", nullable: false),
                    CostType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SlNo = table.Column<int>(type: "int", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixedFurnituresCostDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FixedFurnituresCostDetails_FixedFurnituresMasters_FixedFurnituresMasterId",
                        column: x => x.FixedFurnituresMasterId,
                        principalTable: "FixedFurnituresMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobWorksCostDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobWorksMasterId = table.Column<int>(type: "int", nullable: false),
                    CostType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SlNo = table.Column<int>(type: "int", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobWorksCostDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobWorksCostDetails_JobWorksMasters_JobWorksMasterId",
                        column: x => x.JobWorksMasterId,
                        principalTable: "JobWorksMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LaminarFlowCostDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LaminarFlowMasterId = table.Column<int>(type: "int", nullable: false),
                    CostType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SlNo = table.Column<int>(type: "int", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaminarFlowCostDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LaminarFlowCostDetails_LaminarFlowMasters_LaminarFlowMasterId",
                        column: x => x.LaminarFlowMasterId,
                        principalTable: "LaminarFlowMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LGSPanelSystemCostDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LGSPanelSystemMasterId = table.Column<int>(type: "int", nullable: false),
                    CostType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SlNo = table.Column<int>(type: "int", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LGSPanelSystemCostDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LGSPanelSystemCostDetails_LGSPanelSystemMasters_LGSPanelSystemMasterId",
                        column: x => x.LGSPanelSystemMasterId,
                        principalTable: "LGSPanelSystemMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LooseFurnituresCostDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LooseFurnituresMasterId = table.Column<int>(type: "int", nullable: false),
                    CostType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SlNo = table.Column<int>(type: "int", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LooseFurnituresCostDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LooseFurnituresCostDetails_LooseFurnituresMasters_LooseFurnituresMasterId",
                        column: x => x.LooseFurnituresMasterId,
                        principalTable: "LooseFurnituresMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PeripheralLightCostDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PeripheralLightMasterId = table.Column<int>(type: "int", nullable: false),
                    CostType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SlNo = table.Column<int>(type: "int", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeripheralLightCostDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeripheralLightCostDetails_PeripheralLightMasters_PeripheralLightMasterId",
                        column: x => x.PeripheralLightMasterId,
                        principalTable: "PeripheralLightMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReturnAirSystemCostDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReturnAirSystemMasterId = table.Column<int>(type: "int", nullable: false),
                    CostType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SlNo = table.Column<int>(type: "int", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnAirSystemCostDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturnAirSystemCostDetails_ReturnAirSystemMasters_ReturnAirSystemMasterId",
                        column: x => x.ReturnAirSystemMasterId,
                        principalTable: "ReturnAirSystemMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SinkCostDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SinkMasterId = table.Column<int>(type: "int", nullable: false),
                    CostType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SlNo = table.Column<int>(type: "int", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinkCostDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SinkCostDetails_SinkMasters_SinkMasterId",
                        column: x => x.SinkMasterId,
                        principalTable: "SinkMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StorageCabinetCostDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StorageCabinetMasterId = table.Column<int>(type: "int", nullable: false),
                    CostType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SlNo = table.Column<int>(type: "int", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageCabinetCostDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StorageCabinetCostDetails_StorageCabinetMasters_StorageCabinetMasterId",
                        column: x => x.StorageCabinetMasterId,
                        principalTable: "StorageCabinetMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WallPanelSystemCostDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WallPanelSystemMasterId = table.Column<int>(type: "int", nullable: false),
                    CostType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SlNo = table.Column<int>(type: "int", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WallPanelSystemCostDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WallPanelSystemCostDetails_WallPanelSystemMasters_WallPanelSystemMasterId",
                        column: x => x.WallPanelSystemMasterId,
                        principalTable: "WallPanelSystemMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WindowsCostDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WindowsMasterId = table.Column<int>(type: "int", nullable: false),
                    CostType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SlNo = table.Column<int>(type: "int", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WindowsCostDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WindowsCostDetails_WindowsMasters_WindowsMasterId",
                        column: x => x.WindowsMasterId,
                        principalTable: "WindowsMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkStationCostDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkStationMasterId = table.Column<int>(type: "int", nullable: false),
                    CostType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SlNo = table.Column<int>(type: "int", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkStationCostDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkStationCostDetails_WorkStationMasters_WorkStationMasterId",
                        column: x => x.WorkStationMasterId,
                        principalTable: "WorkStationMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AVSUCostDetails_AVSUMasterId",
                table: "AVSUCostDetails",
                column: "AVSUMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_BHUCostDetails_BHUMasterId",
                table: "BHUCostDetails",
                column: "BHUMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_CeilingSystemCostDetails_CeilingSystemMasterId",
                table: "CeilingSystemCostDetails",
                column: "CeilingSystemMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_CountersCostDetails_CountersMasterId",
                table: "CountersCostDetails",
                column: "CountersMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_CSSDFurnituresCostDetails_CSSDFurnituresMasterId",
                table: "CSSDFurnituresCostDetails",
                column: "CSSDFurnituresMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_DoorsCostDetails_DoorsMasterId",
                table: "DoorsCostDetails",
                column: "DoorsMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedFurnituresCostDetails_FixedFurnituresMasterId",
                table: "FixedFurnituresCostDetails",
                column: "FixedFurnituresMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_JobWorksCostDetails_JobWorksMasterId",
                table: "JobWorksCostDetails",
                column: "JobWorksMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_LaminarFlowCostDetails_LaminarFlowMasterId",
                table: "LaminarFlowCostDetails",
                column: "LaminarFlowMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_LGSPanelSystemCostDetails_LGSPanelSystemMasterId",
                table: "LGSPanelSystemCostDetails",
                column: "LGSPanelSystemMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_LooseFurnituresCostDetails_LooseFurnituresMasterId",
                table: "LooseFurnituresCostDetails",
                column: "LooseFurnituresMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_PeripheralLightCostDetails_PeripheralLightMasterId",
                table: "PeripheralLightCostDetails",
                column: "PeripheralLightMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnAirSystemCostDetails_ReturnAirSystemMasterId",
                table: "ReturnAirSystemCostDetails",
                column: "ReturnAirSystemMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_SinkCostDetails_SinkMasterId",
                table: "SinkCostDetails",
                column: "SinkMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_StorageCabinetCostDetails_StorageCabinetMasterId",
                table: "StorageCabinetCostDetails",
                column: "StorageCabinetMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_WallPanelSystemCostDetails_WallPanelSystemMasterId",
                table: "WallPanelSystemCostDetails",
                column: "WallPanelSystemMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_WindowsCostDetails_WindowsMasterId",
                table: "WindowsCostDetails",
                column: "WindowsMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkStationCostDetails_WorkStationMasterId",
                table: "WorkStationCostDetails",
                column: "WorkStationMasterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AVSUCostDetails");

            migrationBuilder.DropTable(
                name: "BHUCostDetails");

            migrationBuilder.DropTable(
                name: "CeilingSystemCostDetails");

            migrationBuilder.DropTable(
                name: "CountersCostDetails");

            migrationBuilder.DropTable(
                name: "CSSDFurnituresCostDetails");

            migrationBuilder.DropTable(
                name: "DoorsCostDetails");

            migrationBuilder.DropTable(
                name: "FixedFurnituresCostDetails");

            migrationBuilder.DropTable(
                name: "JobWorksCostDetails");

            migrationBuilder.DropTable(
                name: "LaminarFlowCostDetails");

            migrationBuilder.DropTable(
                name: "LGSPanelSystemCostDetails");

            migrationBuilder.DropTable(
                name: "LooseFurnituresCostDetails");

            migrationBuilder.DropTable(
                name: "PeripheralLightCostDetails");

            migrationBuilder.DropTable(
                name: "ReturnAirSystemCostDetails");

            migrationBuilder.DropTable(
                name: "SinkCostDetails");

            migrationBuilder.DropTable(
                name: "StorageCabinetCostDetails");

            migrationBuilder.DropTable(
                name: "WallPanelSystemCostDetails");

            migrationBuilder.DropTable(
                name: "WindowsCostDetails");

            migrationBuilder.DropTable(
                name: "WorkStationCostDetails");

            migrationBuilder.DropTable(
                name: "AVSUMasters");

            migrationBuilder.DropTable(
                name: "BHUMasters");

            migrationBuilder.DropTable(
                name: "CeilingSystemMasters");

            migrationBuilder.DropTable(
                name: "CountersMasters");

            migrationBuilder.DropTable(
                name: "CSSDFurnituresMasters");

            migrationBuilder.DropTable(
                name: "DoorsMasters");

            migrationBuilder.DropTable(
                name: "FixedFurnituresMasters");

            migrationBuilder.DropTable(
                name: "JobWorksMasters");

            migrationBuilder.DropTable(
                name: "LaminarFlowMasters");

            migrationBuilder.DropTable(
                name: "LGSPanelSystemMasters");

            migrationBuilder.DropTable(
                name: "LooseFurnituresMasters");

            migrationBuilder.DropTable(
                name: "PeripheralLightMasters");

            migrationBuilder.DropTable(
                name: "ReturnAirSystemMasters");

            migrationBuilder.DropTable(
                name: "SinkMasters");

            migrationBuilder.DropTable(
                name: "StorageCabinetMasters");

            migrationBuilder.DropTable(
                name: "WallPanelSystemMasters");

            migrationBuilder.DropTable(
                name: "WindowsMasters");

            migrationBuilder.DropTable(
                name: "WorkStationMasters");
        }
    }
}
