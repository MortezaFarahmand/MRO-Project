using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartManagement.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class Modified_ReceivingInspection_And_StoreZone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AtaChapterId",
                table: "StoreZones",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "AtaChapterId",
                table: "StoreZones",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
