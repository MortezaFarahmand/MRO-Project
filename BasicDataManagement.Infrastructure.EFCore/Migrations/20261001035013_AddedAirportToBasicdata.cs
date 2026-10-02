using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasicDataManagement.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class AddedAirportToBasicdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                        //.Annotation("SqlServer:Identity", "1, 1"),
                    Ident = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Latitude_deg = table.Column<float>(type: "real", nullable: false),
                    Longitude_deg = table.Column<float>(type: "real", nullable: false),
                    Elevation_ft = table.Column<int>(type: "int", nullable: false),
                    Continent = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Iso_country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Iso_region = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Municipality = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Scheduled_service = table.Column<bool>(type: "bit", nullable: false),
                    Icao_code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Iata_code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Gps_code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Local_code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Home_link = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Wikipedia_link = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Keywords = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Airports");
        }
    }
}
