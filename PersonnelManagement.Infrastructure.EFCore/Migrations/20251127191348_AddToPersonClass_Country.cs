using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonnelManagement.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class AddToPersonClass_Country : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BirthCountryId",
                table: "Persons",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "BirthProvinceId",
                table: "Persons",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthCountryId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "BirthProvinceId",
                table: "Persons");
        }
    }
}
