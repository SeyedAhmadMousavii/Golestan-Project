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
            migrationBuilder.CreateTable(
                name: "Classrooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Building = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Room_Number = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classrooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Building = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Budget = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Time_Slots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Start_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End_Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Time_Slots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    First_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Last_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hashed_password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoursId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Final_Exam_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Department_Id = table.Column<int>(type: "int", nullable: false),
                    section_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Departments_Department_Id",
                        column: x => x.Department_Id,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Instructor_Id = table.Column<int>(type: "int", nullable: false),
                    User_Id = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Hire_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Department_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instructors_Departments_Department_Id",
                        column: x => x.Department_Id,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Instructors_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Student_Id = table.Column<int>(type: "int", nullable: false),
                    User_Id = table.Column<int>(type: "int", nullable: false),
                    Enrollment_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Depatment_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Departments_Depatment_Id",
                        column: x => x.Depatment_Id,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Roles",
                columns: table => new
                {
                    User_Id = table.Column<int>(type: "int", nullable: false),
                    Role_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Roles", x => new { x.User_Id, x.Role_Id });
                    table.ForeignKey(
                        name: "FK_User_Roles_Roles_Role_Id",
                        column: x => x.Role_Id,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Roles_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Course_Id = table.Column<int>(type: "int", nullable: false),
                    Semester = table.Column<int>(type: "int", nullable: false),
                    year = table.Column<int>(type: "int", nullable: false),
                    Classroom_Id = table.Column<int>(type: "int", nullable: false),
                    Time_Slot_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_Classrooms_Classroom_Id",
                        column: x => x.Classroom_Id,
                        principalTable: "Classrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sections_Courses_Course_Id",
                        column: x => x.Course_Id,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sections_Time_Slots_Time_Slot_Id",
                        column: x => x.Time_Slot_Id,
                        principalTable: "Time_Slots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Takes",
                columns: table => new
                {
                    Student_Id = table.Column<int>(type: "int", nullable: false),
                    Section_Id = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Takes", x => new { x.Student_Id, x.Section_Id });
                    table.ForeignKey(
                        name: "FK_Takes_Sections_Section_Id",
                        column: x => x.Section_Id,
                        principalTable: "Sections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Takes_Students_Student_Id",
                        column: x => x.Student_Id,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Teaches",
                columns: table => new
                {
                    Instructor_Id = table.Column<int>(type: "int", nullable: false),
                    Section_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teaches", x => new { x.Instructor_Id, x.Section_Id });
                    table.ForeignKey(
                        name: "FK_Teaches_Instructors_Instructor_Id",
                        column: x => x.Instructor_Id,
                        principalTable: "Instructors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Teaches_Sections_Section_Id",
                        column: x => x.Section_Id,
                        principalTable: "Sections",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Classrooms",
                columns: new[] { "Id", "Building", "Capacity", "Room_Number" },
                values: new object[,]
                {
                    { 11, "کلاس 11", 20, 1 },
                    { 22, "کلاس 22", 25, 2 },
                    { 33, "کلاس 33", 15, 3 },
                    { 44, "کلاس 44", 30, 4 },
                    { 55, "کلاس 55", 50, 5 }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Budget", "Building", "Name" },
                values: new object[,]
                {
                    { 111, 50000000m, "0015", "کامپیوتر" },
                    { 222, 100000000m, "0154", "مکانیک" },
                    { 333, 150000000m, "1023", "برق" },
                    { 444, 25000000m, "4457", "معماری" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "Time_Slots",
                columns: new[] { "Id", "Day", "End_Time", "Start_Time" },
                values: new object[,]
                {
                    { 1, "شنبه", new DateTime(1, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 7, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "شنبه", new DateTime(1, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "شنبه", new DateTime(1, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "شنبه", new DateTime(1, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 14, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "شنبه", new DateTime(1, 1, 1, 17, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "یکشنبه", new DateTime(1, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 7, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "یکشنبه", new DateTime(1, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "یکشنبه", new DateTime(1, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "یکشنبه", new DateTime(1, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 14, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "یکشنبه", new DateTime(1, 1, 1, 17, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, "دوشنبه", new DateTime(1, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 7, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 12, "دوشنبه", new DateTime(1, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, "دوشنبه", new DateTime(1, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 14, "دوشنبه", new DateTime(1, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 14, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 15, "دوشنبه", new DateTime(1, 1, 1, 17, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, "سه شنبه", new DateTime(1, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 7, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 17, "سه شنبه", new DateTime(1, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, "سه شنبه", new DateTime(1, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 19, "سه شنبه", new DateTime(1, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 14, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 20, "سه شنبه", new DateTime(1, 1, 1, 17, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, "چهارشنبه", new DateTime(1, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 7, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 22, "چهارشنبه", new DateTime(1, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23, "چهارشنبه", new DateTime(1, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 24, "چهارشنبه", new DateTime(1, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 14, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 25, "چهارشنبه", new DateTime(1, 1, 1, 17, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 26, "پنجشنبه", new DateTime(1, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 7, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 27, "پنجشنبه", new DateTime(1, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 28, "پنجشنبه", new DateTime(1, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 29, "پنجشنبه", new DateTime(1, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 14, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 30, "پنجشنبه", new DateTime(1, 1, 1, 17, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Created_at", "Email", "First_name", "Hashed_password", "Last_name", "UserId" },
                values: new object[,]
                {
                    { 10, new DateTime(2000, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "@teach", "Teacher", "1234", "T", 10203050 },
                    { 20, new DateTime(2000, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "@Styd", "Student", "1234", "S", 10203060 },
                    { 10203040, new DateTime(2000, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System@gmai", "mananger", "1234", "system", 10203040 }
                });

            migrationBuilder.InsertData(
                table: "User_Roles",
                columns: new[] { "Role_Id", "User_Id" },
                values: new object[,]
                {
                    { 2, 10 },
                    { 1, 20 },
                    { 3, 10203040 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Department_Id",
                table: "Courses",
                column: "Department_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_Department_Id",
                table: "Instructors",
                column: "Department_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_User_Id",
                table: "Instructors",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_Classroom_Id",
                table: "Sections",
                column: "Classroom_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_Course_Id",
                table: "Sections",
                column: "Course_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sections_Time_Slot_Id",
                table: "Sections",
                column: "Time_Slot_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Depatment_Id",
                table: "Students",
                column: "Depatment_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Students_User_Id",
                table: "Students",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Takes_Section_Id",
                table: "Takes",
                column: "Section_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teaches_Section_Id",
                table: "Teaches",
                column: "Section_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Roles_Role_Id",
                table: "User_Roles",
                column: "Role_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Takes");

            migrationBuilder.DropTable(
                name: "Teaches");

            migrationBuilder.DropTable(
                name: "User_Roles");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Classrooms");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Time_Slots");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
