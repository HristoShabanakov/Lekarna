using Microsoft.EntityFrameworkCore.Migrations;

namespace Lekarna.Data.Migrations
{
    public partial class FkOrderItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderItemId",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderItemId",
                table: "Orders",
                column: "OrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderItems_OrderItemId",
                table: "Orders",
                column: "OrderItemId",
                principalTable: "OrderItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderItems_OrderItemId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderItemId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderItemId",
                table: "Orders");
        }
    }
}
