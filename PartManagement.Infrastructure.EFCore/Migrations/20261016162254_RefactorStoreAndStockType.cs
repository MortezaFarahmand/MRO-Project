using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartManagement.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class RefactorStoreAndStockType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stores_StockTypes_StockTypeId",
                table: "Stores");

            migrationBuilder.RenameColumn(
                name: "StockTypeId",
                table: "Stores",
                newName: "DefaultStockTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Stores_StockTypeId",
                table: "Stores",
                newName: "IX_Stores_DefaultStockTypeId");

            migrationBuilder.CreateTable(
                name: "StoreStockTypes",
                columns: table => new
                {
                    StoreId = table.Column<long>(type: "bigint", nullable: false),
                    StockTypeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreStockTypes", x => new { x.StoreId, x.StockTypeId });
                    table.ForeignKey(
                        name: "FK_StoreStockTypes_StockTypes_StockTypeId",
                        column: x => x.StockTypeId,
                        principalTable: "StockTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreStockTypes_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreStockTypes_StockTypeId",
                table: "StoreStockTypes",
                column: "StockTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_StockTypes_DefaultStockTypeId",
                table: "Stores",
                column: "DefaultStockTypeId",
                principalTable: "StockTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stores_StockTypes_DefaultStockTypeId",
                table: "Stores");

            migrationBuilder.DropTable(
                name: "StoreStockTypes");

            migrationBuilder.RenameColumn(
                name: "DefaultStockTypeId",
                table: "Stores",
                newName: "StockTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Stores_DefaultStockTypeId",
                table: "Stores",
                newName: "IX_Stores_StockTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_StockTypes_StockTypeId",
                table: "Stores",
                column: "StockTypeId",
                principalTable: "StockTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
