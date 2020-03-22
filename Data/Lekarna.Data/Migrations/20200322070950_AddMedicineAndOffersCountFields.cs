namespace Lekarna.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddMedicineAndOffersCountFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Suppliers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Medicine",
                table: "Offers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OffersCount",
                table: "Offers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_UserId",
                table: "Suppliers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_AspNetUsers_UserId",
                table: "Suppliers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_AspNetUsers_UserId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_UserId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Medicine",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "OffersCount",
                table: "Offers");
        }
    }
}
