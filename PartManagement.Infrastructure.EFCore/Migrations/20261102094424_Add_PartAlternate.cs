using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartManagement.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class Add_PartAlternate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PartAlternates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrimaryPartId = table.Column<long>(type: "bigint", nullable: false),
                    AlternatePartId = table.Column<long>(type: "bigint", nullable: false),
                    RelationType = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    AlternateCategory = table.Column<int>(type: "int", maxLength: 50, nullable: true),
                    PreferenceLevel = table.Column<int>(type: "int", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: true),
                    EffectiveFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EffectiveTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartAlternates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartAlternates_Parts_AlternatePartId",
                        column: x => x.AlternatePartId,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartAlternates_Parts_PrimaryPartId",
                        column: x => x.PrimaryPartId,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartAlternates_AlternatePartId",
                table: "PartAlternates",
                column: "AlternatePartId");

            migrationBuilder.CreateIndex(
                name: "IX_PartAlternates_PrimaryPartId_AlternatePartId",
                table: "PartAlternates",
                columns: new[] { "PrimaryPartId", "AlternatePartId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartAlternates");
        }
    }
}
