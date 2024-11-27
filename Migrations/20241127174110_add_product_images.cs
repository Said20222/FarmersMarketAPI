using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmersMarketAPI.Migrations
{
    /// <inheritdoc />
    public partial class add_product_images : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FarmerApproved",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductImage",
                columns: table => new
                {
                    ProductImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImage", x => x.ProductImageId);
                    table.ForeignKey(
                        name: "FK_ProductImage_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"),
                columns: new[] { "FarmerApproved", "PasswordHash" },
                values: new object[] { null, "AQAAAAIAAAPoAAAAEIg9LNKzFNsL4pxVZI1jGU2fHp5lfVykjNgiP88yxu/Sf0fCFfFEBOgEoIzrHqUo8Q==" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductId",
                table: "ProductImage",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImage");

            migrationBuilder.DropColumn(
                name: "FarmerApproved",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"),
                column: "PasswordHash",
                value: "AQAAAAIAAAPoAAAAEKrpTBiDXAXbLQkH9mu/Wmfotwb1MakCshqiAiu/SGBQwcWpQVdfkyDRjaEmuVV/cg==");
        }
    }
}
