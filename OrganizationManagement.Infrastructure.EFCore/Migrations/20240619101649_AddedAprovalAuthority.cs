using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasicDatanManagement.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class AddedAprovalAuthority : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ApprovalAuthorityId",
                table: "OrganizationPictures",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApprovalAuthorities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEn = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NameFa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(700)", maxLength: 700, nullable: true),
                    WebSite = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LogoPicture = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    LogoPictureAlt = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LogoPictureTitle = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CanonicalAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Keywords = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CountryId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalAuthorities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalAuthorities_Countrys_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countrys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationPictures_ApprovalAuthorityId",
                table: "OrganizationPictures",
                column: "ApprovalAuthorityId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalAuthorities_CountryId",
                table: "ApprovalAuthorities",
                column: "CountryId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationPictures_ApprovalAuthorities_ApprovalAuthorityId",
                table: "OrganizationPictures",
                column: "ApprovalAuthorityId",
                principalTable: "ApprovalAuthorities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationPictures_ApprovalAuthorities_ApprovalAuthorityId",
                table: "OrganizationPictures");

            migrationBuilder.DropTable(
                name: "ApprovalAuthorities");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationPictures_ApprovalAuthorityId",
                table: "OrganizationPictures");

            migrationBuilder.DropColumn(
                name: "ApprovalAuthorityId",
                table: "OrganizationPictures");
        }
    }
}
