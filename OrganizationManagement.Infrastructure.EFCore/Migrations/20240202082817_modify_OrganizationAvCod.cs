using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizationManagement.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class modify_OrganizationAvCod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Feild_1",
                table: "OrganizationAviationCodes");

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "OrganizationAviationCodes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "OrganizationAviationCodes");

            migrationBuilder.AddColumn<string>(
                name: "Feild_1",
                table: "OrganizationAviationCodes",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }
    }
}
