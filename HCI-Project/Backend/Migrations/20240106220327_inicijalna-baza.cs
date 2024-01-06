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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gosti");

            migrationBuilder.DropTable(
                name: "Menadzeri");

            migrationBuilder.DropTable(
                name: "Sobe");

            migrationBuilder.DropTable(
                name: "KorisnickiNalozi");
        }
    }
}
