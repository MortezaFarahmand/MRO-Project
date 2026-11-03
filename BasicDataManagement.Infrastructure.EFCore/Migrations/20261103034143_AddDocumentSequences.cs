using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasicDataManagement.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class AddDocumentSequences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "Slides",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Slides",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Slides",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedBy",
                table: "Slides",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "Provinces",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Provinces",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Provinces",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedBy",
                table: "Provinces",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "PictureCategorys",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PictureCategorys",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "PictureCategorys",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedBy",
                table: "PictureCategorys",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "Entitis",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Entitis",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Entitis",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedBy",
                table: "Entitis",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "Countrys",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Countrys",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Countrys",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedBy",
                table: "Countrys",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "Citys",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Citys",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Citys",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedBy",
                table: "Citys",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "Airports",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Airports",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Airports",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedBy",
                table: "Airports",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DocumentSequences",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    BaseId = table.Column<int>(type: "int", nullable: true),
                    LastValue = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentSequences", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentSequences_DocumentType_Year_BaseId",
                table: "DocumentSequences",
                columns: new[] { "DocumentType", "Year", "BaseId" },
                unique: true,
                filter: "[BaseId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentSequences");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Slides");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Slides");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Slides");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Slides");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Provinces");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Provinces");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Provinces");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Provinces");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PictureCategorys");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PictureCategorys");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "PictureCategorys");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "PictureCategorys");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Entitis");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Entitis");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Entitis");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Entitis");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Countrys");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Countrys");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Countrys");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Countrys");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Citys");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Citys");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Citys");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Citys");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Airports");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Airports");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Airports");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Airports");
        }
    }
}
