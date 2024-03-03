using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class dodangostidurezervacije : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GostId",
                table: "ZauzeteSobe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ZauzeteSobe_GostId",
                table: "ZauzeteSobe",
                column: "GostId");

            migrationBuilder.AddForeignKey(
                name: "FK_ZauzeteSobe_Gosti_GostId",
                table: "ZauzeteSobe",
                column: "GostId",
                principalTable: "Gosti",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZauzeteSobe_Gosti_GostId",
                table: "ZauzeteSobe");

            migrationBuilder.DropIndex(
                name: "IX_ZauzeteSobe_GostId",
                table: "ZauzeteSobe");

            migrationBuilder.DropColumn(
                name: "GostId",
                table: "ZauzeteSobe");
        }
    }
}
