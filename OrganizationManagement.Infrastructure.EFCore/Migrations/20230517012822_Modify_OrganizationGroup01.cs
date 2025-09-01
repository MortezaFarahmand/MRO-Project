using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasicDatanManagement.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class Modify_OrganizationGroup01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Keywords",
                table: "OrganizationGroups",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaDescription",
                table: "OrganizationGroups",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureAlt",
                table: "OrganizationGroups",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureTitle",
                table: "OrganizationGroups",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "OrganizationGroups",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Keywords",
                table: "OrganizationGroups");

            migrationBuilder.DropColumn(
                name: "MetaDescription",
                table: "OrganizationGroups");

            migrationBuilder.DropColumn(
                name: "PictureAlt",
                table: "OrganizationGroups");

            migrationBuilder.DropColumn(
                name: "PictureTitle",
                table: "OrganizationGroups");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "OrganizationGroups");
        }
    }
}
