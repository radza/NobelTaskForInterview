using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NobelTaskForInterview.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    game_started_at = table.Column<DateTime>(type: "text", nullable: false, defaultValueSql: "datetime('now')"),
                    game_finished_at = table.Column<DateTime>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Moves",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    game_id = table.Column<int>(type: "integer", nullable: false),
                    player_sign = table.Column<long>(type: "integer", nullable: false),
                    computer_sign = table.Column<long>(type: "integer", nullable: false),
                    result = table.Column<long>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moves", x => x.id);
                    table.ForeignKey(
                        name: "FK_Moves_Games_game_id",
                        column: x => x.game_id,
                        principalTable: "Games",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Moves_game_id",
                table: "Moves",
                column: "game_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Moves");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
