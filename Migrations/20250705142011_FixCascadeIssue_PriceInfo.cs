﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebSocket_API.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadeIssue_PriceInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_PriceInfos_LatestPriceId",
                table: "Assets");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_PriceInfos_LatestPriceId",
                table: "Assets",
                column: "LatestPriceId",
                principalTable: "PriceInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_PriceInfos_LatestPriceId",
                table: "Assets");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_PriceInfos_LatestPriceId",
                table: "Assets",
                column: "LatestPriceId",
                principalTable: "PriceInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
