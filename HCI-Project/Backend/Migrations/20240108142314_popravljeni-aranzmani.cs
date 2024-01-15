using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class popravljeniaranzmani : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aranzmani_Sobe_SobaId",
                table: "Aranzmani");

            migrationBuilder.DropIndex(
                name: "IX_Aranzmani_SobaId",
                table: "Aranzmani");

            migrationBuilder.DropColumn(
                name: "Doplata",
                table: "Aranzmani");

            migrationBuilder.DropColumn(
                name: "SobaId",
                table: "Aranzmani");

            migrationBuilder.CreateTable(
                name: "SobeAranzmani",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SobaId = table.Column<int>(type: "int", nullable: false),
                    AranzmanId = table.Column<int>(type: "int", nullable: false),
                    Doplata = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SobeAranzmani", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SobeAranzmani_Aranzmani_AranzmanId",
                        column: x => x.AranzmanId,
                        principalTable: "Aranzmani",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SobeAranzmani_Sobe_SobaId",
                        column: x => x.SobaId,
                        principalTable: "Sobe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SobeAranzmani_AranzmanId",
                table: "SobeAranzmani",
                column: "AranzmanId");

            migrationBuilder.CreateIndex(
                name: "IX_SobeAranzmani_SobaId",
                table: "SobeAranzmani",
                column: "SobaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SobeAranzmani");

            migrationBuilder.AddColumn<float>(
                name: "Doplata",
                table: "Aranzmani",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "SobaId",
                table: "Aranzmani",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Aranzmani_SobaId",
                table: "Aranzmani",
                column: "SobaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aranzmani_Sobe_SobaId",
                table: "Aranzmani",
                column: "SobaId",
                principalTable: "Sobe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
