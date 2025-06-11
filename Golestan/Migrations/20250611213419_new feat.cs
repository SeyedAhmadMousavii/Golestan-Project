using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Golestan.Migrations
{
    /// <inheritdoc />
    public partial class newfeat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "instructorname",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "instructorname",
                table: "Instructors");
        }
    }
}
