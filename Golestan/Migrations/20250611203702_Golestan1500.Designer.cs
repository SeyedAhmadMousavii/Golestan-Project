﻿// <auto-generated />
using System;
using Golestan.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Golestan.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250611203702_Golestan1500")]
    partial class Golestan1500
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Golestan.Models.Classrooms", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Building")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<int>("Room_Number")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("Classrooms");

                    b.HasData(
                        new
                        {
                            Id = 11,
                            Building = "کلاس 11",
                            Capacity = 20,
                            EndTime = new TimeSpan(0, 0, 0, 0, 0),
                            Room_Number = 1,
                            StartTime = new TimeSpan(0, 0, 0, 0, 0)
                        },
                        new
                        {
                            Id = 22,
                            Building = "کلاس 22",
                            Capacity = 25,
                            EndTime = new TimeSpan(0, 0, 0, 0, 0),
                            Room_Number = 2,
                            StartTime = new TimeSpan(0, 0, 0, 0, 0)
                        },
                        new
                        {
                            Id = 33,
                            Building = "کلاس 33",
                            Capacity = 15,
                            EndTime = new TimeSpan(0, 0, 0, 0, 0),
                            Room_Number = 3,
                            StartTime = new TimeSpan(0, 0, 0, 0, 0)
                        },
                        new
                        {
                            Id = 44,
                            Building = "کلاس 44",
                            Capacity = 30,
                            EndTime = new TimeSpan(0, 0, 0, 0, 0),
                            Room_Number = 4,
                            StartTime = new TimeSpan(0, 0, 0, 0, 0)
                        },
                        new
                        {
                            Id = 55,
                            Building = "کلاس 55",
                            Capacity = 50,
                            EndTime = new TimeSpan(0, 0, 0, 0, 0),
                            Room_Number = 5,
                            StartTime = new TimeSpan(0, 0, 0, 0, 0)
                        });
                });

            modelBuilder.Entity("Golestan.Models.Courses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CoursId")
                        .HasColumnType("int");

                    b.Property<int>("Department_Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Final_Exam_Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Prerequisite")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("section_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Department_Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Golestan.Models.Departments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Budget")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Building")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            Id = 111,
                            Budget = 50000000m,
                            Building = "0015",
                            Name = "کامپیوتر"
                        },
                        new
                        {
                            Id = 222,
                            Budget = 100000000m,
                            Building = "0154",
                            Name = "مکانیک"
                        },
                        new
                        {
                            Id = 333,
                            Budget = 150000000m,
                            Building = "1023",
                            Name = "برق"
                        },
                        new
                        {
                            Id = 444,
                            Budget = 25000000m,
                            Building = "4457",
                            Name = "معماری"
                        });
                });

            modelBuilder.Entity("Golestan.Models.Instructors", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Department_Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Hire_Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Instructor_Id")
                        .HasColumnType("int");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("User_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Department_Id");

                    b.HasIndex("User_Id");

                    b.ToTable("Instructors");
                });

            modelBuilder.Entity("Golestan.Models.Roles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Name")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = 1
                        },
                        new
                        {
                            Id = 2,
                            Name = 2
                        },
                        new
                        {
                            Id = 3,
                            Name = 3
                        });
                });

            modelBuilder.Entity("Golestan.Models.Sections", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("Classroom_Id")
                        .HasColumnType("int");

                    b.Property<int>("Course_Id")
                        .HasColumnType("int");

                    b.Property<int>("SectionId")
                        .HasColumnType("int");

                    b.Property<int>("Semester")
                        .HasColumnType("int");

                    b.Property<int>("Time_Slot_Id")
                        .HasColumnType("int");

                    b.Property<string>("coursetitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Classroom_Id");

                    b.HasIndex("Course_Id")
                        .IsUnique();

                    b.HasIndex("Time_Slot_Id");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("Golestan.Models.Students", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Depatment_Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Enrollment_Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Student_Id")
                        .HasColumnType("int");

                    b.Property<int>("User_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Depatment_Id");

                    b.HasIndex("User_Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Golestan.Models.Takes", b =>
                {
                    b.Property<int>("Student_Id")
                        .HasColumnType("int");

                    b.Property<int>("Section_Id")
                        .HasColumnType("int");

                    b.Property<string>("Grade")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Student_Id", "Section_Id");

                    b.HasIndex("Section_Id");

                    b.ToTable("Takes");
                });

            modelBuilder.Entity("Golestan.Models.Teaches", b =>
                {
                    b.Property<int>("Instructor_Id")
                        .HasColumnType("int");

                    b.Property<int>("Section_Id")
                        .HasColumnType("int");

                    b.HasKey("Instructor_Id", "Section_Id");

                    b.HasIndex("Section_Id")
                        .IsUnique();

                    b.ToTable("Teaches");
                });

            modelBuilder.Entity("Golestan.Models.Time_Slots", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("End_Time")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Start_Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Time_Slots");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Day = "شنبه",
                            End_Time = new DateTime(1, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            Start_Time = new DateTime(1, 1, 1, 7, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Day = "شنبه",
                            End_Time = new DateTime(1, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified),
                            Start_Time = new DateTime(1, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            Day = "شنبه",
                            End_Time = new DateTime(1, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            Start_Time = new DateTime(1, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            Day = "شنبه",
                            End_Time = new DateTime(1, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            Start_Time = new DateTime(1, 1, 1, 14, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5,
                            Day = "شنبه",
                            End_Time = new DateTime(1, 1, 1, 17, 30, 0, 0, DateTimeKind.Unspecified),
                            Start_Time = new DateTime(1, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 6,
                            Day = "یکشنبه",
                            End_Time = new DateTime(1, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            Start_Time = new DateTime(1, 1, 1, 7, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 7,
                            Day = "یکشنبه",
                            End_Time = new DateTime(1, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified),
                            Start_Time = new DateTime(1, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 8,
                            Day = "یکشنبه",
                            End_Time = new DateTime(1, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            Start_Time = new DateTime(1, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 9,
                            Day = "یکشنبه",
                            End_Time = new DateTime(1, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            Start_Time = new DateTime(1, 1, 1, 14, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 10,
                            Day = "یکشنبه",
                            End_Time = new DateTime(1, 1, 1, 17, 30, 0, 0, DateTimeKind.Unspecified),
                            Start_Time = new DateTime(1, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 11,
                            Day = "دوشنبه",
                            End_Time = new DateTime(1, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            Start_Time = new DateTime(1, 1, 1, 7, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 12,
                            Day = "دوشنبه",
                            End_Time = new DateTime(1, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified),
                            Start_Time = new DateTime(1, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 13,
                            Day = "دوشنبه",
                            End_Time = new DateTime(1, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            Start_Time = new DateTime(1, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 14,
                            Day = "دوشنبه",
                            End_Time = new DateTime(1, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            Start_Time = new DateTime(1, 1, 1, 14, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 15,
                            Day = "دوشنبه",
                            End_Time = new DateTime(1, 1, 1, 17, 30, 0, 0, DateTimeKind.Unspecified),
                            Start_Time = new DateTime(1, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 16,
                            Day = "سه شنبه",
                            End_Time = new DateTime(1, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            Start_Time = new DateTime(1, 1, 1, 7, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 17,
                            Day = "سه شنبه",
                            End_Time = new DateTime(1, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified),
                            Start_Time = new DateTime(1, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 18,
                            Day = "سه شنبه",
                            End_Time = new DateTime(1, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            Start_Time = new DateTime(1, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 19,
                            Day = "سه شنبه",
                            End_Time = new DateTime(1, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            Start_Time = new DateTime(1, 1, 1, 14, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 20,
                            Day = "سه شنبه",
                            End_Time = new DateTime(1, 1, 1, 17, 30, 0, 0, DateTimeKind.Unspecified),
                            Start_Time = new DateTime(1, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 21,
                            Day = "چهارشنبه",
                            End_Time = new DateTime(1, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            Start_Time = new DateTime(1, 1, 1, 7, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 22,
                            Day = "چهارشنبه",
                            End_Time = new DateTime(1, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified),
                            Start_Time = new DateTime(1, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 23,
                            Day = "چهارشنبه",
                            End_Time = new DateTime(1, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            Start_Time = new DateTime(1, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 24,
                            Day = "چهارشنبه",
                            End_Time = new DateTime(1, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            Start_Time = new DateTime(1, 1, 1, 14, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 25,
                            Day = "چهارشنبه",
                            End_Time = new DateTime(1, 1, 1, 17, 30, 0, 0, DateTimeKind.Unspecified),
                            Start_Time = new DateTime(1, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 26,
                            Day = "پنجشنبه",
                            End_Time = new DateTime(1, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            Start_Time = new DateTime(1, 1, 1, 7, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 27,
                            Day = "پنجشنبه",
                            End_Time = new DateTime(1, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified),
                            Start_Time = new DateTime(1, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 28,
                            Day = "پنجشنبه",
                            End_Time = new DateTime(1, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            Start_Time = new DateTime(1, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 29,
                            Day = "پنجشنبه",
                            End_Time = new DateTime(1, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            Start_Time = new DateTime(1, 1, 1, 14, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 30,
                            Day = "پنجشنبه",
                            End_Time = new DateTime(1, 1, 1, 17, 30, 0, 0, DateTimeKind.Unspecified),
                            Start_Time = new DateTime(1, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Golestan.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("First_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hashed_password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Last_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 10203040,
                            Created_at = new DateTime(2000, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "System@gmai",
                            First_name = "mananger",
                            Hashed_password = "1234",
                            Last_name = "system",
                            UserId = 10203040
                        });
                });

            modelBuilder.Entity("User_Role", b =>
                {
                    b.Property<int>("User_Id")
                        .HasColumnType("int");

                    b.Property<int>("Role_Id")
                        .HasColumnType("int");

                    b.HasKey("User_Id", "Role_Id");

                    b.HasIndex("Role_Id");

                    b.ToTable("User_Roles");

                    b.HasData(
                        new
                        {
                            User_Id = 10203040,
                            Role_Id = 3
                        });
                });

            modelBuilder.Entity("Golestan.Models.Courses", b =>
                {
                    b.HasOne("Golestan.Models.Departments", "departments")
                        .WithMany("courses")
                        .HasForeignKey("Department_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("departments");
                });

            modelBuilder.Entity("Golestan.Models.Instructors", b =>
                {
                    b.HasOne("Golestan.Models.Departments", "departments")
                        .WithMany("instructors")
                        .HasForeignKey("Department_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Golestan.Models.Users", "User")
                        .WithMany("instructors")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("departments");
                });

            modelBuilder.Entity("Golestan.Models.Sections", b =>
                {
                    b.HasOne("Golestan.Models.Classrooms", "classrooms")
                        .WithMany("sections")
                        .HasForeignKey("Classroom_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Golestan.Models.Courses", "courses")
                        .WithOne("section")
                        .HasForeignKey("Golestan.Models.Sections", "Course_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Golestan.Models.Time_Slots", "time_slots")
                        .WithMany("sections")
                        .HasForeignKey("Time_Slot_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("classrooms");

                    b.Navigation("courses");

                    b.Navigation("time_slots");
                });

            modelBuilder.Entity("Golestan.Models.Students", b =>
                {
                    b.HasOne("Golestan.Models.Departments", "departments")
                        .WithMany("students")
                        .HasForeignKey("Depatment_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Golestan.Models.Users", "User")
                        .WithMany("students")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("departments");
                });

            modelBuilder.Entity("Golestan.Models.Takes", b =>
                {
                    b.HasOne("Golestan.Models.Sections", "sections")
                        .WithMany("takes")
                        .HasForeignKey("Section_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Golestan.Models.Students", "students")
                        .WithMany("takes")
                        .HasForeignKey("Student_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("sections");

                    b.Navigation("students");
                });

            modelBuilder.Entity("Golestan.Models.Teaches", b =>
                {
                    b.HasOne("Golestan.Models.Instructors", "instructors")
                        .WithMany("teaches")
                        .HasForeignKey("Instructor_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Golestan.Models.Sections", "sections")
                        .WithOne("teaches")
                        .HasForeignKey("Golestan.Models.Teaches", "Section_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("instructors");

                    b.Navigation("sections");
                });

            modelBuilder.Entity("User_Role", b =>
                {
                    b.HasOne("Golestan.Models.Roles", "roles")
                        .WithMany("User_Roles")
                        .HasForeignKey("Role_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Golestan.Models.Users", "users")
                        .WithMany("User_Roles")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("roles");

                    b.Navigation("users");
                });

            modelBuilder.Entity("Golestan.Models.Classrooms", b =>
                {
                    b.Navigation("sections");
                });

            modelBuilder.Entity("Golestan.Models.Courses", b =>
                {
                    b.Navigation("section");
                });

            modelBuilder.Entity("Golestan.Models.Departments", b =>
                {
                    b.Navigation("courses");

                    b.Navigation("instructors");

                    b.Navigation("students");
                });

            modelBuilder.Entity("Golestan.Models.Instructors", b =>
                {
                    b.Navigation("teaches");
                });

            modelBuilder.Entity("Golestan.Models.Roles", b =>
                {
                    b.Navigation("User_Roles");
                });

            modelBuilder.Entity("Golestan.Models.Sections", b =>
                {
                    b.Navigation("takes");

                    b.Navigation("teaches")
                        .IsRequired();
                });

            modelBuilder.Entity("Golestan.Models.Students", b =>
                {
                    b.Navigation("takes");
                });

            modelBuilder.Entity("Golestan.Models.Time_Slots", b =>
                {
                    b.Navigation("sections");
                });

            modelBuilder.Entity("Golestan.Models.Users", b =>
                {
                    b.Navigation("User_Roles");

                    b.Navigation("instructors");

                    b.Navigation("students");
                });
#pragma warning restore 612, 618
        }
    }
}
