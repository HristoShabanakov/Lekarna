namespace Lekarna.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddMedicineDBModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicine_Offers_OfferId",
                table: "Medicine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Medicine",
                table: "Medicine");

            migrationBuilder.RenameTable(
                name: "Medicine",
                newName: "Medicines");

            migrationBuilder.RenameIndex(
                name: "IX_Medicine_OfferId",
                table: "Medicines",
                newName: "IX_Medicines_OfferId");

            migrationBuilder.RenameIndex(
                name: "IX_Medicine_IsDeleted",
                table: "Medicines",
                newName: "IX_Medicines_IsDeleted");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Categories",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Medicines",
                table: "Medicines",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserId",
                table: "Categories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_UserId",
                table: "Categories",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Offers_OfferId",
                table: "Medicines",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_UserId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Offers_OfferId",
                table: "Medicines");

            migrationBuilder.DropIndex(
                name: "IX_Categories_UserId",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Medicines",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Medicines",
                newName: "Medicine");

            migrationBuilder.RenameIndex(
                name: "IX_Medicines_OfferId",
                table: "Medicine",
                newName: "IX_Medicine_OfferId");

            migrationBuilder.RenameIndex(
                name: "IX_Medicines_IsDeleted",
                table: "Medicine",
                newName: "IX_Medicine_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Medicine",
                table: "Medicine",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicine_Offers_OfferId",
                table: "Medicine",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
