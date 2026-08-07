using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizationManagement.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class CreateOrganizationPosition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrganizationPositions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 100000, nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(max)", maxLength: 100000, nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Activate = table.Column<bool>(type: "bit", nullable: false),
                    ParentPositionId = table.Column<long>(type: "bigint", nullable: true),
                    OrganizationDepartmentId = table.Column<long>(type: "bigint", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationPositions_OrganizationDepartments_OrganizationDepartmentId",
                        column: x => x.OrganizationDepartmentId,
                        principalTable: "OrganizationDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationPositions_OrganizationDepartmentId",
                table: "OrganizationPositions",
                column: "OrganizationDepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganizationPositions");
        }
    }
}
