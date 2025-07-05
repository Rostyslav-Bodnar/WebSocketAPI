using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebSocket_API.Migrations
{
    /// <inheritdoc />
    public partial class PriceInfoUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Assets_LatestPriceId",
                table: "Assets");

            migrationBuilder.AddColumn<decimal>(
                name: "Change",
                table: "PriceInfos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ChangePct",
                table: "PriceInfos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Volume",
                table: "PriceInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "LatestPriceId",
                table: "Assets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_LatestPriceId",
                table: "Assets",
                column: "LatestPriceId",
                unique: true,
                filter: "[LatestPriceId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Assets_LatestPriceId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Change",
                table: "PriceInfos");

            migrationBuilder.DropColumn(
                name: "ChangePct",
                table: "PriceInfos");

            migrationBuilder.DropColumn(
                name: "Volume",
                table: "PriceInfos");

            migrationBuilder.AlterColumn<int>(
                name: "LatestPriceId",
                table: "Assets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assets_LatestPriceId",
                table: "Assets",
                column: "LatestPriceId",
                unique: true);
        }
    }
}
