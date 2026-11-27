using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokerLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPokerBetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "table",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pokerHand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pokerHand", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pokerHand_table_TableId",
                        column: x => x.TableId,
                        principalTable: "table",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HandId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bet_player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "player",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bet_pokerHand_HandId",
                        column: x => x.HandId,
                        principalTable: "pokerHand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bet_HandId",
                table: "bet",
                column: "HandId");

            migrationBuilder.CreateIndex(
                name: "IX_bet_PlayerId",
                table: "bet",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_pokerHand_TableId",
                table: "pokerHand",
                column: "TableId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bet");

            migrationBuilder.DropTable(
                name: "pokerHand");

            migrationBuilder.DropTable(
                name: "table");
        }
    }
}
