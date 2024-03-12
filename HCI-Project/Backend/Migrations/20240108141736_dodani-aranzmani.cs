using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class dodaniaranzmani : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aranzmani",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SobaId = table.Column<int>(type: "int", nullable: false),
                    NazivAranzmana = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Doplata = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aranzmani", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aranzmani_Sobe_SobaId",
                        column: x => x.SobaId,
                        principalTable: "Sobe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ZauzeteSobe_SobaId",
                table: "ZauzeteSobe",
                column: "SobaId");

            migrationBuilder.CreateIndex(
                name: "IX_Aranzmani_SobaId",
                table: "Aranzmani",
                column: "SobaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ZauzeteSobe_Sobe_SobaId",
                table: "ZauzeteSobe",
                column: "SobaId",
                principalTable: "Sobe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZauzeteSobe_Sobe_SobaId",
                table: "ZauzeteSobe");

            migrationBuilder.DropTable(
                name: "Aranzmani");

            migrationBuilder.DropIndex(
                name: "IX_ZauzeteSobe_SobaId",
                table: "ZauzeteSobe");
        }
    }
}
