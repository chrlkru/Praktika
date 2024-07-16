using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Praktika.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOrderListIdFromDiscontCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                   name: "FK_DiscontCard_OrderList_OrderListId",
                   table: "DiscontCard");

            migrationBuilder.DropColumn(
                name: "OrderListId",
                table: "DiscontCard");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
         name: "OrderListId",
         table: "DiscontCard",
         type: "INTEGER",
         nullable: false,
         defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscontCard_OrderList_OrderListId",
                table: "DiscontCard",
                column: "OrderListId",
                principalTable: "OrderList",
                principalColumn: "OrderListId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
