using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebSocket_API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGicsItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GicsParentId",
                table: "GicsItems",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GicsParentId",
                table: "GicsItems");
        }
    }
}
