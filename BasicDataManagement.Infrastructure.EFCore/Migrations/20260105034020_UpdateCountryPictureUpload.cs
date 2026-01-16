using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasicDataManagement.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCountryPictureUpload : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "Countrys");

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Countrys",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Countrys");

            migrationBuilder.AddColumn<long>(
                name: "PictureId",
                table: "Countrys",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
