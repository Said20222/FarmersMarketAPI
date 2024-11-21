using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmersMarketAPI.Migrations
{
    /// <inheritdoc />
    public partial class added_missing_properties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrderStatus",
                table: "MarketOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"),
                column: "PasswordHash",
                value: "AQAAAAIAAAPoAAAAEIwKSin3UBUU5xotbCFMZJ+zeBcf0REaxf1Khc9z8/d2MyC/oasrqzItsOOWBymv4g==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "MarketOrders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"),
                column: "PasswordHash",
                value: "AQAAAAIAAAPoAAAAELvFzid7lqcLUoBvTcdIZGW16Nww5mPWyMVHoBNg/YOoNKObKVXjUMJgdG87rz7SpA==");
        }
    }
}
