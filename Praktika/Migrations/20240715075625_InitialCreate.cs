using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Praktika.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
        name: "Users",
        columns: table => new
        {
            UsersId = table.Column<int>(nullable: false)
                .Annotation("Sqlite:Autoincrement", true),
            Фио = table.Column<string>(nullable: false),
            Телефон = table.Column<string>(nullable: false),
            Email = table.Column<string>(nullable: false),
            ДатаРождения = table.Column<DateTime>(nullable: false)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_Users", x => x.UsersId);
        });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscontCard");

            migrationBuilder.DropTable(
                name: "OrderList");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
