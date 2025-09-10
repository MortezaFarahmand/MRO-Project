using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizationManagement.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class AddedPersonAndSlide : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PersonId",
                table: "Countrys",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEn = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NameFa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FamilyEn = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FamilyFa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Birthday = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PassportNo = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    NationalCode = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    Attachment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PictureAlt = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IDCartNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Activate = table.Column<bool>(type: "bit", nullable: false),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Countrys_PersonId",
                table: "Countrys",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_OrganizationId",
                table: "Persons",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Countrys_Persons_PersonId",
                table: "Countrys",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countrys_Persons_PersonId",
                table: "Countrys");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Countrys_PersonId",
                table: "Countrys");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Countrys");
        }
    }
}
