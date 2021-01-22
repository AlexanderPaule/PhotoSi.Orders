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
                name: "CategoryEntity",
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
                    table.PrimaryKey("PK_CategoryEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderEntity",
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
                    table.PrimaryKey("PK_OrderEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderEntity_CategoryEntity_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "sales",
                        principalTable: "CategoryEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductEntity",
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
                    table.PrimaryKey("PK_ProductEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductEntity_CategoryEntity_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "sales",
                        principalTable: "CategoryEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OptionEntity",
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
                    table.PrimaryKey("PK_OptionEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OptionEntity_ProductEntity_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "sales",
                        principalTable: "ProductEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderedProductEntity",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DbCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DbUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderedProductEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderedProductEntity_OrderEntity_OrderEntityId",
                        column: x => x.OrderEntityId,
                        principalSchema: "sales",
                        principalTable: "OrderEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderedProductEntity_ProductEntity_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "sales",
                        principalTable: "ProductEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderedOptionEntity",
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
                    table.PrimaryKey("PK_OrderedOptionEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderedOptionEntity_OptionEntity_OptionId",
                        column: x => x.OptionId,
                        principalSchema: "sales",
                        principalTable: "OptionEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderedOptionEntity_OrderedProductEntity_OrderedProductId",
                        column: x => x.OrderedProductId,
                        principalSchema: "sales",
                        principalTable: "OrderedProductEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OptionEntity_ProductId",
                schema: "sales",
                table: "OptionEntity",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedOptionEntity_OptionId",
                schema: "sales",
                table: "OrderedOptionEntity",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedOptionEntity_OrderedProductId",
                schema: "sales",
                table: "OrderedOptionEntity",
                column: "OrderedProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedProductEntity_OrderEntityId",
                schema: "sales",
                table: "OrderedProductEntity",
                column: "OrderEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedProductEntity_ProductId",
                schema: "sales",
                table: "OrderedProductEntity",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderEntity_CategoryId",
                schema: "sales",
                table: "OrderEntity",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductEntity_CategoryId",
                schema: "sales",
                table: "ProductEntity",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderedOptionEntity",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "OptionEntity",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "OrderedProductEntity",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "OrderEntity",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "ProductEntity",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "CategoryEntity",
                schema: "sales");
        }
    }
}
