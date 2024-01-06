using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class inicijalnabaza : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KorisnickiNalozi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SlikaKorisnika = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnickiNalozi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kreveti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZaOsoba = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kreveti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sobe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Slike = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojGostiju = table.Column<int>(type: "int", nullable: false),
                    Klima = table.Column<bool>(type: "bit", nullable: false),
                    Bazen = table.Column<bool>(type: "bit", nullable: false),
                    Spa = table.Column<bool>(type: "bit", nullable: false),
                    PrilagodjenInvalidima = table.Column<bool>(type: "bit", nullable: false),
                    Teretana = table.Column<bool>(type: "bit", nullable: false),
                    DozvoljeniLjubimci = table.Column<bool>(type: "bit", nullable: false),
                    Minibar = table.Column<bool>(type: "bit", nullable: false),
                    Balkon = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sobe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gosti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    BrojRezervacija = table.Column<int>(type: "int", nullable: false),
                    Drzava = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gosti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gosti_KorisnickiNalozi_Id",
                        column: x => x.Id,
                        principalTable: "KorisnickiNalozi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Menadzeri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menadzeri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menadzeri_KorisnickiNalozi_Id",
                        column: x => x.Id,
                        principalTable: "KorisnickiNalozi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SobeKreveti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SobaId = table.Column<int>(type: "int", nullable: false),
                    KrevetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SobeKreveti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SobeKreveti_Kreveti_KrevetId",
                        column: x => x.KrevetId,
                        principalTable: "Kreveti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SobeKreveti_Sobe_SobaId",
                        column: x => x.SobaId,
                        principalTable: "Sobe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recenzije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SobaId = table.Column<int>(type: "int", nullable: false),
                    GostId = table.Column<int>(type: "int", nullable: false),
                    Ocjena = table.Column<int>(type: "int", nullable: false),
                    Komentar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recenzije", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recenzije_Gosti_GostId",
                        column: x => x.GostId,
                        principalTable: "Gosti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recenzije_Sobe_SobaId",
                        column: x => x.SobaId,
                        principalTable: "Sobe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recenzije_GostId",
                table: "Recenzije",
                column: "GostId");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzije_SobaId",
                table: "Recenzije",
                column: "SobaId");

            migrationBuilder.CreateIndex(
                name: "IX_SobeKreveti_KrevetId",
                table: "SobeKreveti",
                column: "KrevetId");

            migrationBuilder.CreateIndex(
                name: "IX_SobeKreveti_SobaId",
                table: "SobeKreveti",
                column: "SobaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Menadzeri");

            migrationBuilder.DropTable(
                name: "Recenzije");

            migrationBuilder.DropTable(
                name: "SobeKreveti");

            migrationBuilder.DropTable(
                name: "Gosti");

            migrationBuilder.DropTable(
                name: "Kreveti");

            migrationBuilder.DropTable(
                name: "Sobe");

            migrationBuilder.DropTable(
                name: "KorisnickiNalozi");
        }
    }
}
