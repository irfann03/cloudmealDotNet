using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class modified_weeklyMenu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartOfWeek",
                table: "WeeklyMenu");

            migrationBuilder.AddColumn<int>(
                name: "KitchenId",
                table: "WeeklyMenu",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyMenu_KitchenId",
                table: "WeeklyMenu",
                column: "KitchenId");

            migrationBuilder.AddForeignKey(
                name: "FK_WeeklyMenu_Kitchen_KitchenId",
                table: "WeeklyMenu",
                column: "KitchenId",
                principalTable: "Kitchen",
                principalColumn: "KitchenId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeeklyMenu_Kitchen_KitchenId",
                table: "WeeklyMenu");

            migrationBuilder.DropIndex(
                name: "IX_WeeklyMenu_KitchenId",
                table: "WeeklyMenu");

            migrationBuilder.DropColumn(
                name: "KitchenId",
                table: "WeeklyMenu");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartOfWeek",
                table: "WeeklyMenu",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
