using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonnelManagement.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class MappingPersonPicture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entiti",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entiti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonPictures",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Picture = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PictureCategoryId = table.Column<long>(type: "bigint", nullable: true),
                    PersonId = table.Column<long>(type: "bigint", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonPictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonPictures_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PictureCategory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntitiId = table.Column<long>(type: "bigint", nullable: false),
                    PersonPictureId = table.Column<long>(type: "bigint", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PictureCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PictureCategory_Entiti_EntitiId",
                        column: x => x.EntitiId,
                        principalTable: "Entiti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PictureCategory_PersonPictures_PersonPictureId",
                        column: x => x.PersonPictureId,
                        principalTable: "PersonPictures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonPictures_PersonId",
                table: "PersonPictures",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PictureCategory_EntitiId",
                table: "PictureCategory",
                column: "EntitiId");

            migrationBuilder.CreateIndex(
                name: "IX_PictureCategory_PersonPictureId",
                table: "PictureCategory",
                column: "PersonPictureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PictureCategory");

            migrationBuilder.DropTable(
                name: "Entiti");

            migrationBuilder.DropTable(
                name: "PersonPictures");
        }
    }
}
