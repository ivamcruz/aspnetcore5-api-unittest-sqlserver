using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Portal.DevTest.Date.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_products",
                columns: table => new
                {
                    idProduct = table.Column<string>(type: "varchar(40)", nullable: false),
                    name = table.Column<string>(type: "varchar(60)", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<double>(type: "float(2)", precision: 2, scale: 7, nullable: false),
                    isActived = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    creationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_products", x => x.idProduct);
                });

            migrationBuilder.CreateTable(
                name: "tb_users",
                columns: table => new
                {
                    idUser = table.Column<string>(type: "varchar(40)", nullable: false),
                    userName = table.Column<string>(type: "varchar(50)", nullable: false),
                    displayName = table.Column<string>(type: "varchar(60)", nullable: false),
                    email = table.Column<string>(type: "varchar(100)", nullable: false),
                    password = table.Column<string>(type: "varchar(50)", nullable: false),
                    isActived = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    creationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_users", x => x.idUser);
                });

            migrationBuilder.CreateTable(
                name: "tb_orders",
                columns: table => new
                {
                    idOrder = table.Column<string>(type: "varchar(40)", nullable: false),
                    idUser = table.Column<string>(type: "varchar(40)", nullable: false),
                    isActived = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    creationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_orders", x => x.idOrder);
                    table.ForeignKey(
                        name: "FK_tb_orders_tb_users_idUser",
                        column: x => x.idUser,
                        principalTable: "tb_users",
                        principalColumn: "idUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_orderItems",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(40)", nullable: false),
                    idOrder = table.Column<string>(type: "varchar(40)", nullable: false),
                    idProduct = table.Column<string>(type: "varchar(40)", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    currentPrice = table.Column<decimal>(type: "decimal(6,5)", precision: 6, scale: 5, nullable: false),
                    isActived = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_orderItems", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_orderItems_tb_orders_idOrder",
                        column: x => x.idOrder,
                        principalTable: "tb_orders",
                        principalColumn: "idOrder",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_orderItems_tb_products_idOrder",
                        column: x => x.idOrder,
                        principalTable: "tb_products",
                        principalColumn: "idProduct",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_orderItems_idOrder",
                table: "tb_orderItems",
                column: "idOrder");

            migrationBuilder.CreateIndex(
                name: "IX_tb_orders_idUser",
                table: "tb_orders",
                column: "idUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_orderItems");

            migrationBuilder.DropTable(
                name: "tb_orders");

            migrationBuilder.DropTable(
                name: "tb_products");

            migrationBuilder.DropTable(
                name: "tb_users");
        }
    }
}
