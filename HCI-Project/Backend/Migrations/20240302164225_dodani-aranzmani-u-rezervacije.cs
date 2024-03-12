using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class dodaniaranzmaniurezervacije : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SobaAranzmanId",
                table: "ZauzeteSobe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ZauzeteSobe_SobaAranzmanId",
                table: "ZauzeteSobe",
                column: "SobaAranzmanId");

            migrationBuilder.AddForeignKey(
                name: "FK_ZauzeteSobe_SobeAranzmani_SobaAranzmanId",
                table: "ZauzeteSobe",
                column: "SobaAranzmanId",
                principalTable: "SobeAranzmani",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZauzeteSobe_SobeAranzmani_SobaAranzmanId",
                table: "ZauzeteSobe");

            migrationBuilder.DropIndex(
                name: "IX_ZauzeteSobe_SobaAranzmanId",
                table: "ZauzeteSobe");

            migrationBuilder.DropColumn(
                name: "SobaAranzmanId",
                table: "ZauzeteSobe");
        }
    }
}
