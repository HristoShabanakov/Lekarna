namespace Lekarna.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class SupplierDbModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SupplierId",
                table: "Offers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_SupplierId",
                table: "Offers",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_IsDeleted",
                table: "Suppliers",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Suppliers_SupplierId",
                table: "Offers",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Suppliers_SupplierId",
                table: "Offers");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_SupplierId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "Offers");
        }
    }
}
