using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoSi.Orders.Server.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sales");

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DbCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DbUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DbCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DbUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "sales",
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DbCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DbUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "sales",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DbCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DbUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Options_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "sales",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderedProducts",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DbCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DbUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderedProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderedProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "sales",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderedProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "sales",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderedOptions",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderedProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DbCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DbUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderedOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderedOptions_Options_OptionId",
                        column: x => x.OptionId,
                        principalSchema: "sales",
                        principalTable: "Options",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderedOptions_OrderedProducts_OrderedProductId",
                        column: x => x.OrderedProductId,
                        principalSchema: "sales",
                        principalTable: "OrderedProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Options_ProductId",
                schema: "sales",
                table: "Options",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedOptions_OptionId",
                schema: "sales",
                table: "OrderedOptions",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedOptions_OrderedProductId",
                schema: "sales",
                table: "OrderedOptions",
                column: "OrderedProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedProducts_OrderId",
                schema: "sales",
                table: "OrderedProducts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedProducts_ProductId",
                schema: "sales",
                table: "OrderedProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CategoryId",
                schema: "sales",
                table: "Orders",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                schema: "sales",
                table: "Products",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderedOptions",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "Options",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "OrderedProducts",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "sales");
        }
    }
}
