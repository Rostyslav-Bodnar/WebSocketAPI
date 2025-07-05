using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebSocket_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exchanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchanges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GicsClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectorId = table.Column<int>(type: "int", nullable: false),
                    IndustryGroupId = table.Column<int>(type: "int", nullable: false),
                    IndustryId = table.Column<int>(type: "int", nullable: false),
                    SubIndustryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GicsClassifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GicsItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GicsItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GicsItems_GicsItems_ParentId",
                        column: x => x.ParentId,
                        principalTable: "GicsItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Kinds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kinds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstrumentProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GicsClassificationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstrumentProfiles_GicsClassifications_GicsClassificationId",
                        column: x => x.GicsClassificationId,
                        principalTable: "GicsClassifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProviderExchanges",
                columns: table => new
                {
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    ExchangeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderExchanges", x => new { x.ProviderId, x.ExchangeId });
                    table.ForeignKey(
                        name: "FK_ProviderExchanges_Exchanges_ExchangeId",
                        column: x => x.ExchangeId,
                        principalTable: "Exchanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProviderExchanges_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LatestPriceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instruments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KindId = table.Column<int>(type: "int", nullable: true),
                    ExchangeId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tickSize = table.Column<double>(type: "float", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstrumentProfileId = table.Column<int>(type: "int", nullable: false),
                    AssetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instruments_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Instruments_Exchanges_ExchangeId",
                        column: x => x.ExchangeId,
                        principalTable: "Exchanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Instruments_InstrumentProfiles_InstrumentProfileId",
                        column: x => x.InstrumentProfileId,
                        principalTable: "InstrumentProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Instruments_Kinds_KindId",
                        column: x => x.KindId,
                        principalTable: "Kinds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "PriceInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceInfos_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstrumentMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProviderId = table.Column<int>(type: "int", nullable: true),
                    ExchangeId = table.Column<int>(type: "int", nullable: true),
                    DefaultOrderSize = table.Column<int>(type: "int", nullable: false),
                    MaxOrderSize = table.Column<int>(type: "int", nullable: true),
                    TradingHoursId = table.Column<int>(type: "int", nullable: true),
                    InstrumentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstrumentMappings_Exchanges_ExchangeId",
                        column: x => x.ExchangeId,
                        principalTable: "Exchanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_InstrumentMappings_Instruments_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstrumentMappings_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TradingHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegularStart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegularEnd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ElectronicStart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ElectronicEnd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstrumentMappingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradingHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradingHours_InstrumentMappings_InstrumentMappingId",
                        column: x => x.InstrumentMappingId,
                        principalTable: "InstrumentMappings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_LatestPriceId",
                table: "Assets",
                column: "LatestPriceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GicsItems_ParentId",
                table: "GicsItems",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentMappings_ExchangeId",
                table: "InstrumentMappings",
                column: "ExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentMappings_InstrumentId",
                table: "InstrumentMappings",
                column: "InstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentMappings_ProviderId",
                table: "InstrumentMappings",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentProfiles_GicsClassificationId",
                table: "InstrumentProfiles",
                column: "GicsClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Instruments_AssetId",
                table: "Instruments",
                column: "AssetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instruments_ExchangeId",
                table: "Instruments",
                column: "ExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_Instruments_InstrumentProfileId",
                table: "Instruments",
                column: "InstrumentProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Instruments_KindId",
                table: "Instruments",
                column: "KindId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceInfos_AssetId",
                table: "PriceInfos",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderExchanges_ExchangeId",
                table: "ProviderExchanges",
                column: "ExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_TradingHours_InstrumentMappingId",
                table: "TradingHours",
                column: "InstrumentMappingId",
                unique: true,
                filter: "[InstrumentMappingId] IS NOT NULL");

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

            migrationBuilder.DropTable(
                name: "GicsItems");

            migrationBuilder.DropTable(
                name: "ProviderExchanges");

            migrationBuilder.DropTable(
                name: "TradingHours");

            migrationBuilder.DropTable(
                name: "InstrumentMappings");

            migrationBuilder.DropTable(
                name: "Instruments");

            migrationBuilder.DropTable(
                name: "Providers");

            migrationBuilder.DropTable(
                name: "Exchanges");

            migrationBuilder.DropTable(
                name: "InstrumentProfiles");

            migrationBuilder.DropTable(
                name: "Kinds");

            migrationBuilder.DropTable(
                name: "GicsClassifications");

            migrationBuilder.DropTable(
                name: "PriceInfos");

            migrationBuilder.DropTable(
                name: "Assets");
        }
    }
}
