using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class popravljenesobe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "BesplatnoOtkazivanje",
                table: "Sobe",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ZauzeteSobe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SobaId = table.Column<int>(type: "int", nullable: false),
                    BrojOsoba = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cijena = table.Column<float>(type: "real", nullable: false),
                    DatumDolaska = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumOdlaska = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZauzeteSobe", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZauzeteSobe");

            migrationBuilder.DropColumn(
                name: "BesplatnoOtkazivanje",
                table: "Sobe");
        }
    }
}
