using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerApp.DataAccess.Migrations
{
    public partial class burgerMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Burgers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    IsVegetarian = table.Column<bool>(type: "bit", nullable: false),
                    IsVegan = table.Column<bool>(type: "bit", nullable: false),
                    HasFries = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Burgers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpenAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CloseAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsDelivered = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BurgerOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    BurgerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BurgerOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BurgerOrder_Burgers_BurgerId",
                        column: x => x.BurgerId,
                        principalTable: "Burgers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BurgerOrder_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Burgers",
                columns: new[] { "Id", "HasFries", "IsVegan", "IsVegetarian", "Name", "Price" },
                values: new object[,]
                {
                    { 1, false, true, true, "Hamburger", 180.0 },
                    { 2, true, false, true, "Cheeseburger", 200.0 },
                    { 3, false, false, false, "Baconburger", 300.0 },
                    { 4, true, false, false, "MegaBurger", 400.0 }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Address", "CloseAt", "Name", "OpenAt" },
                values: new object[,]
                {
                    { 1, "kicevo", new DateTime(2023, 10, 6, 22, 0, 0, 0, DateTimeKind.Local), "kicburger", new DateTime(2023, 10, 6, 8, 0, 0, 0, DateTimeKind.Local) },
                    { 2, "bitola", new DateTime(2023, 10, 6, 22, 0, 0, 0, DateTimeKind.Local), "bitburger", new DateTime(2023, 10, 6, 8, 0, 0, 0, DateTimeKind.Local) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "FirstName", "LastName", "Password", "Role", "UserName" },
                values: new object[,]
                {
                    { 1, "Trpe Blazeski", "Sime", "Nokoski", "j?b??????tN???", "superAdmin", "sime123!" },
                    { 2, "Rudnicka", "Marko", "Markoski", "?8??%7??CI'P??", "customer", "marko123" },
                    { 3, "8Septemvri", "Rozeta", "Markoska", "?c???q?'?S\rACO", "customer", "rozeta123" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "IsDelivered", "LocationId", "UserId" },
                values: new object[] { 1, false, 1, 1 });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "IsDelivered", "LocationId", "UserId" },
                values: new object[] { 2, false, 2, 3 });

            migrationBuilder.InsertData(
                table: "BurgerOrder",
                columns: new[] { "Id", "BurgerId", "OrderId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "BurgerOrder",
                columns: new[] { "Id", "BurgerId", "OrderId" },
                values: new object[] { 2, 4, 1 });

            migrationBuilder.InsertData(
                table: "BurgerOrder",
                columns: new[] { "Id", "BurgerId", "OrderId" },
                values: new object[] { 3, 2, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_BurgerOrder_BurgerId",
                table: "BurgerOrder",
                column: "BurgerId");

            migrationBuilder.CreateIndex(
                name: "IX_BurgerOrder_OrderId",
                table: "BurgerOrder",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_LocationId",
                table: "Orders",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BurgerOrder");

            migrationBuilder.DropTable(
                name: "Burgers");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
