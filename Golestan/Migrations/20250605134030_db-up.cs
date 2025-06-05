using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Golestan.Migrations
{
    /// <inheritdoc />
    public partial class dbup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Instructor_Id",
                table: "Instructors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Instructor_Id",
                table: "Instructors");
        }
    }
}
