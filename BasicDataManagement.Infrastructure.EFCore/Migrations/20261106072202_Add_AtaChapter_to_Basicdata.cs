using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasicDataManagement.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class Add_AtaChapter_to_Basicdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AtaChapters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TitleFa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Group = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtaChapters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaintenancePageBlocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TitleFa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    StartPage = table.Column<int>(type: "int", nullable: false),
                    EndPage = table.Column<int>(type: "int", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenancePageBlocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AtaSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AtaChapterId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    FullCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TitleFa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtaSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AtaSections_AtaChapters_AtaChapterId",
                        column: x => x.AtaChapterId,
                        principalTable: "AtaChapters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AtaSubjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AtaSectionId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    FullCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TitleFa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    WdmChapterSection = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    IpcChapterSection = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtaSubjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AtaSubjects_AtaSections_AtaSectionId",
                        column: x => x.AtaSectionId,
                        principalTable: "AtaSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AtaChapters_Code",
                table: "AtaChapters",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AtaSections_AtaChapterId",
                table: "AtaSections",
                column: "AtaChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_AtaSections_FullCode",
                table: "AtaSections",
                column: "FullCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AtaSubjects_AtaSectionId",
                table: "AtaSubjects",
                column: "AtaSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_AtaSubjects_FullCode",
                table: "AtaSubjects",
                column: "FullCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaintenancePageBlocks_Code",
                table: "MaintenancePageBlocks",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AtaSubjects");

            migrationBuilder.DropTable(
                name: "MaintenancePageBlocks");

            migrationBuilder.DropTable(
                name: "AtaSections");

            migrationBuilder.DropTable(
                name: "AtaChapters");
        }
    }
}
