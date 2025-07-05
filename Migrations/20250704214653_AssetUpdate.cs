using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebSocket_API.Migrations
{
    /// <inheritdoc />
    public partial class AssetUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instruments_Assets_AssetId",
                table: "Instruments");

            migrationBuilder.DropIndex(
                name: "IX_Instruments_AssetId",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "Instruments");

            migrationBuilder.AddColumn<int>(
                name: "InstrumentId",
                table: "Assets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProviderId",
                table: "Assets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Assets_InstrumentId",
                table: "Assets",
                column: "InstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_ProviderId",
                table: "Assets",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Instruments_InstrumentId",
                table: "Assets",
                column: "InstrumentId",
                principalTable: "Instruments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Providers_ProviderId",
                table: "Assets",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Instruments_InstrumentId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Providers_ProviderId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_InstrumentId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_ProviderId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "InstrumentId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "Assets");

            migrationBuilder.AddColumn<int>(
                name: "AssetId",
                table: "Instruments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instruments_AssetId",
                table: "Instruments",
                column: "AssetId",
                unique: true,
                filter: "[AssetId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Instruments_Assets_AssetId",
                table: "Instruments",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
