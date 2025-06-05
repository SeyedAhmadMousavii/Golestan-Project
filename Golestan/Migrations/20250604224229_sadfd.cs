using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Golestan.Migrations
{
    /// <inheritdoc />
    public partial class sadfd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teaches_Section_Id",
                table: "Teaches");

            migrationBuilder.DropIndex(
                name: "IX_Takes_Section_Id",
                table: "Takes");

            migrationBuilder.CreateIndex(
                name: "IX_Teaches_Section_Id",
                table: "Teaches",
                column: "Section_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Takes_Section_Id",
                table: "Takes",
                column: "Section_Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teaches_Section_Id",
                table: "Teaches");

            migrationBuilder.DropIndex(
                name: "IX_Takes_Section_Id",
                table: "Takes");

            migrationBuilder.CreateIndex(
                name: "IX_Teaches_Section_Id",
                table: "Teaches",
                column: "Section_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Takes_Section_Id",
                table: "Takes",
                column: "Section_Id");
        }
    }
}
