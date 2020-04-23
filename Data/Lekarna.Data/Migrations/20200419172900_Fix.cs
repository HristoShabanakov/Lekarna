namespace Lekarna.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Medicines_MedicineId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_MedicineId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "MedicineId",
                table: "Offers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MedicineId",
                table: "Offers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offers_MedicineId",
                table: "Offers",
                column: "MedicineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Medicines_MedicineId",
                table: "Offers",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
