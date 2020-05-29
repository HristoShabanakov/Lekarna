namespace Lekarna.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddDiscountAndTargetModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "Target",
                table: "Medicines");

            migrationBuilder.AddColumn<string>(
                name: "DiscountId",
                table: "Medicines",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TargetId",
                table: "Medicines",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Targets",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Targets", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_DiscountId",
                table: "Medicines",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_TargetId",
                table: "Medicines",
                column: "TargetId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_IsDeleted",
                table: "Discounts",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Targets_IsDeleted",
                table: "Targets",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Discounts_DiscountId",
                table: "Medicines",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Targets_TargetId",
                table: "Medicines",
                column: "TargetId",
                principalTable: "Targets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Discounts_DiscountId",
                table: "Medicines");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Targets_TargetId",
                table: "Medicines");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Targets");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_DiscountId",
                table: "Medicines");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_TargetId",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "TargetId",
                table: "Medicines");

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "Medicines",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Target",
                table: "Medicines",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
