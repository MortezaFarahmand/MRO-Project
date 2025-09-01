using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasicDatanManagement.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class AviationOrgCode_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrganizationAviationCodes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ICAO = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IATA = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CivilAutority = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CallSign = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CountryId = table.Column<long>(type: "bigint", maxLength: 255, nullable: false),
                    Feild_1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationAviationCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationAviationCodes_Countrys_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countrys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationAviationCodes_CountryId",
                table: "OrganizationAviationCodes",
                column: "CountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganizationAviationCodes");
        }
    }
}
