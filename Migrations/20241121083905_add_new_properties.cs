using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmersMarketAPI.Migrations
{
    /// <inheritdoc />
    public partial class add_new_properties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarketOrders_AspNetUsers_BuyerId",
                table: "MarketOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "FarmDescription",
                table: "Farms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"),
                columns: new[] { "DeliveryAddress", "PasswordHash" },
                values: new object[] { null, "AQAAAAIAAAPoAAAAELvFzid7lqcLUoBvTcdIZGW16Nww5mPWyMVHoBNg/YOoNKObKVXjUMJgdG87rz7SpA==" });

            migrationBuilder.AddForeignKey(
                name: "FK_MarketOrders_AspNetUsers_BuyerId",
                table: "MarketOrders",
                column: "BuyerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarketOrders_AspNetUsers_BuyerId",
                table: "MarketOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "FarmDescription",
                table: "Farms");

            migrationBuilder.DropColumn(
                name: "DeliveryAddress",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"),
                column: "PasswordHash",
                value: "AQAAAAIAAAPoAAAAEIwFlJ0VX0l1PsYk6Z0vXSD9BZ2x4ccaPE8n+KBiaTUs/pqVyBWFEXMD5njRJfOFWQ==");

            migrationBuilder.AddForeignKey(
                name: "FK_MarketOrders_AspNetUsers_BuyerId",
                table: "MarketOrders",
                column: "BuyerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
