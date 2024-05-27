using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProjet.Migrations
{
    /// <inheritdoc />
    public partial class addCommandeModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommandeId",
                table: "PanierArts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Commande",
                columns: table => new
                {
                    CommandeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCommande = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commande", x => x.CommandeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PanierArts_CommandeId",
                table: "PanierArts",
                column: "CommandeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PanierArts_Commande_CommandeId",
                table: "PanierArts",
                column: "CommandeId",
                principalTable: "Commande",
                principalColumn: "CommandeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PanierArts_Commande_CommandeId",
                table: "PanierArts");

            migrationBuilder.DropTable(
                name: "Commande");

            migrationBuilder.DropIndex(
                name: "IX_PanierArts_CommandeId",
                table: "PanierArts");

            migrationBuilder.DropColumn(
                name: "CommandeId",
                table: "PanierArts");
        }
    }
}
