using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebSocket_API.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Instruments_AssetId",
                table: "Instruments");

            migrationBuilder.AlterColumn<int>(
                name: "AssetId",
                table: "Instruments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Instruments_AssetId",
                table: "Instruments",
                column: "AssetId",
                unique: true,
                filter: "[AssetId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Instruments_AssetId",
                table: "Instruments");

            migrationBuilder.AlterColumn<int>(
                name: "AssetId",
                table: "Instruments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instruments_AssetId",
                table: "Instruments",
                column: "AssetId",
                unique: true);
        }
    }
}
