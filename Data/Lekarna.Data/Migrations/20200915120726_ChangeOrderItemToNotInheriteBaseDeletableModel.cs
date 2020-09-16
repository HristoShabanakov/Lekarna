namespace Lekarna.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangeOrderItemToNotInheriteBaseDeletableModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrdersItems_IsDeleted",
                table: "OrdersItems");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "OrdersItems");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "OrdersItems");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrdersItems");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "OrdersItems");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "OrdersItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "OrdersItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "OrdersItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "OrdersItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "OrdersItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "OrdersItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrdersItems_IsDeleted",
                table: "OrdersItems",
                column: "IsDeleted");
        }
    }
}
