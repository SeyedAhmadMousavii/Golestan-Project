using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Golestan.Migrations
{
    /// <inheritdoc />
    public partial class a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User_Roles",
                keyColumns: new[] { "Role_Id", "User_Id" },
                keyValues: new object[] { 2, 10 });

            migrationBuilder.DeleteData(
                table: "User_Roles",
                keyColumns: new[] { "Role_Id", "User_Id" },
                keyValues: new object[] { 1, 20 });

            migrationBuilder.DeleteData(
                table: "User_Roles",
                keyColumns: new[] { "Role_Id", "User_Id" },
                keyValues: new object[] { 2, 40302010 });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 40302010);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Created_at", "Email", "First_name", "Hashed_password", "Last_name", "UserId" },
                values: new object[,]
                {
                    { 10, new DateTime(2000, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "@teach", "Teacher", "1234", "T", 10203050 },
                    { 20, new DateTime(2000, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "@Styd", "Student", "1234", "S", 10203060 },
                    { 40302010, new DateTime(2000, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System1@gmai", "mananger1", "4321", "system1", 0 }
                });

            migrationBuilder.InsertData(
                table: "User_Roles",
                columns: new[] { "Role_Id", "User_Id" },
                values: new object[,]
                {
                    { 2, 10 },
                    { 1, 20 },
                    { 2, 40302010 }
                });
        }
    }
}
