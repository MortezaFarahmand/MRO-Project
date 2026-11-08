using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartManagement.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class ReceivingInspection_And_StoreZone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Placement_Mode",
                table: "Stores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Placement_UseAircraftModel",
                table: "Stores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Placement_UseAta",
                table: "Stores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Placement_UseEnvClass",
                table: "Stores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Placement_UseKeyword",
                table: "Stores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Placement_UseLocationKind",
                table: "Stores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Placement_UsePartPhysical",
                table: "Stores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Placement_UseStockType",
                table: "Stores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Receiving_AllowOneStep",
                table: "Stores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Receiving_DeadlineHours",
                table: "Stores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Receiving_HoldZoneId",
                table: "Stores",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Receiving_InspectionMode",
                table: "Stores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "AtaChapterId",
                table: "Parts",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HeightCm",
                table: "Parts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LengthCm",
                table: "Parts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PreferredEnvironment",
                table: "Parts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WeightKg",
                table: "Parts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WidthCm",
                table: "Parts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CurrentZoneId",
                table: "InventoryItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InspectionStatus",
                table: "InventoryItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ReceivingInspectionItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    HelpText = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    AllowNotApplicable = table.Column<bool>(type: "bit", nullable: false),
                    ApplicableKeyword = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StockTypeId = table.Column<long>(type: "bigint", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceivingInspectionItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceivingInspectionResults",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiptId = table.Column<long>(type: "bigint", nullable: false),
                    ReceiptLineId = table.Column<long>(type: "bigint", nullable: true),
                    InventoryItemId = table.Column<long>(type: "bigint", nullable: true),
                    StoreId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    InspectorUserId = table.Column<long>(type: "bigint", nullable: true),
                    InspectedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeadlineAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    TotalSteps = table.Column<int>(type: "int", nullable: false),
                    CompletedSteps = table.Column<int>(type: "int", nullable: false),
                    CurrentStepIndex = table.Column<int>(type: "int", nullable: false),
                    IsOverridden = table.Column<bool>(type: "bit", nullable: false),
                    OverrideByUserId = table.Column<long>(type: "bigint", nullable: true),
                    OverrideAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OverrideReason = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceivingInspectionResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoreZones",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ZoneType = table.Column<int>(type: "int", nullable: false),
                    AtaChapterId = table.Column<long>(type: "bigint", nullable: true),
                    KeywordGroup = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AircraftModelId = table.Column<long>(type: "bigint", nullable: true),
                    LocationKind = table.Column<int>(type: "int", nullable: true),
                    EnvironmentalClass = table.Column<int>(type: "int", nullable: true),
                    MaxWeightKg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaxVolumeM3 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreZones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceivingInspectionChecks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResultId = table.Column<long>(type: "bigint", nullable: false),
                    InspectionItemId = table.Column<long>(type: "bigint", nullable: false),
                    StepIndex = table.Column<int>(type: "int", nullable: false),
                    Outcome = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    AnsweredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AnsweredByUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceivingInspectionChecks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceivingInspectionChecks_ReceivingInspectionItems_InspectionItemId",
                        column: x => x.InspectionItemId,
                        principalTable: "ReceivingInspectionItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceivingInspectionChecks_ReceivingInspectionResults_ResultId",
                        column: x => x.ResultId,
                        principalTable: "ReceivingInspectionResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReceivingInspectionChecks_InspectionItemId",
                table: "ReceivingInspectionChecks",
                column: "InspectionItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivingInspectionChecks_ResultId",
                table: "ReceivingInspectionChecks",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivingInspectionItems_Code",
                table: "ReceivingInspectionItems",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoreZones_StoreId_Code",
                table: "StoreZones",
                columns: new[] { "StoreId", "Code" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceivingInspectionChecks");

            migrationBuilder.DropTable(
                name: "StoreZones");

            migrationBuilder.DropTable(
                name: "ReceivingInspectionItems");

            migrationBuilder.DropTable(
                name: "ReceivingInspectionResults");

            migrationBuilder.DropColumn(
                name: "Placement_Mode",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Placement_UseAircraftModel",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Placement_UseAta",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Placement_UseEnvClass",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Placement_UseKeyword",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Placement_UseLocationKind",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Placement_UsePartPhysical",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Placement_UseStockType",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Receiving_AllowOneStep",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Receiving_DeadlineHours",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Receiving_HoldZoneId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Receiving_InspectionMode",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "AtaChapterId",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "HeightCm",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "LengthCm",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "PreferredEnvironment",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "WeightKg",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "WidthCm",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "CurrentZoneId",
                table: "InventoryItems");

            migrationBuilder.DropColumn(
                name: "InspectionStatus",
                table: "InventoryItems");
        }
    }
}
