using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceFeedback.Migrations
{
    public partial class InitialSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "dbo",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", nullable: true),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    Category = table.Column<string>(type: "varchar(100)", nullable: false),
                    ActiveIndicator = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    AuditDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "GETDATE()"),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    ShippingAddress = table.Column<string>(type: "varchar(400)", nullable: false),
                    BillingAddress = table.Column<string>(type: "varchar(400)", nullable: false),
                    PaymentDetails = table.Column<string>(type: "varchar(400)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserCart",
                schema: "dbo",
                columns: table => new
                {
                    UserCartId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ProductPurchased = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    OrderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderStaus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "GETDATE()"),
                    PurchasedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCart", x => x.UserCartId);
                    table.ForeignKey(
                        name: "FK_UserCart_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCart",
                schema: "dbo",
                columns: table => new
                {
                    CartId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    UserCartId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCart", x => x.CartId);
                    table.ForeignKey(
                        name: "FK_ShoppingCart_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "dbo",
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingCart_UserCart_UserCartId",
                        column: x => x.UserCartId,
                        principalSchema: "dbo",
                        principalTable: "UserCart",
                        principalColumn: "UserCartId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_ProductId",
                schema: "dbo",
                table: "ShoppingCart",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_UserCartId",
                schema: "dbo",
                table: "ShoppingCart",
                column: "UserCartId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCart_UserId",
                schema: "dbo",
                table: "UserCart",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingCart",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserCart",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "User",
                schema: "dbo");
        }
    }
}
