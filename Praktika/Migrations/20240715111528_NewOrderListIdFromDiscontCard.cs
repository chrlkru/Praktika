using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Praktika.Migrations
{
    /// <inheritdoc />
    public partial class NewOrderListIdFromDiscontCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DiscontCard_OrderListId",
                table: "DiscontCard",
                column: "OrderListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DiscontCard_OrderListId",
                table: "DiscontCard");
        }
    }
}
