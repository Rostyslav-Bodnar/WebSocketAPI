using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebSocket_API.Migrations
{
    /// <inheritdoc />
    public partial class Cascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceInfos_Assets_AssetId",
                table: "PriceInfos");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceInfos_Assets_AssetId",
                table: "PriceInfos",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceInfos_Assets_AssetId",
                table: "PriceInfos");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceInfos_Assets_AssetId",
                table: "PriceInfos",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
