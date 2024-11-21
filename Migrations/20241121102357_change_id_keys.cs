using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmersMarketAPI.Migrations
{
    /// <inheritdoc />
    public partial class change_id_keys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "OfferId",
                table: "Offers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "MarketOrders",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "FarmId",
                table: "Farms",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Categories",
                newName: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"),
                column: "PasswordHash",
                value: "AQAAAAIAAAPoAAAAEKrpTBiDXAXbLQkH9mu/Wmfotwb1MakCshqiAiu/SGBQwcWpQVdfkyDRjaEmuVV/cg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Offers",
                newName: "OfferId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MarketOrders",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Farms",
                newName: "FarmId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "CategoryId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"),
                column: "PasswordHash",
                value: "AQAAAAIAAAPoAAAAEIwKSin3UBUU5xotbCFMZJ+zeBcf0REaxf1Khc9z8/d2MyC/oasrqzItsOOWBymv4g==");
        }
    }
}
