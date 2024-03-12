using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class dodanacijenaispravak : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cijene",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SobaId = table.Column<int>(type: "int", nullable: false),
                    BrojOsoba = table.Column<int>(type: "int", nullable: false),
                    CijenaSobe = table.Column<float>(type: "real", nullable: false),
                    DjecaDo = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cijene", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cijene_Sobe_SobaId",
                        column: x => x.SobaId,
                        principalTable: "Sobe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cijene_SobaId",
                table: "Cijene",
                column: "SobaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cijene");
        }
    }
}
