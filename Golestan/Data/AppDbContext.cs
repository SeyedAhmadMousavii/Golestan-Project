using Golestan.Models;
using Microsoft.EntityFrameworkCore;

namespace Golestan.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<User_Role> User_Roles { get; set; }
        public DbSet<Students> Students { get; set; }
        public DbSet<Instructors> Instructors { get; set; }
        public DbSet<Teaches> Teaches { get; set; }
        public DbSet<Takes> Takes { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Sections> Sections { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Classrooms> Classrooms { get; set; }
        public DbSet<Time_Slots> Time_Slots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //                                      Role cunstructor
            modelBuilder.Entity<Roles>().HasData
                (
                new Roles { Id = 1, Name = role.Student },
                new Roles { Id = 2, Name = role.Teacher },
                new Roles { Id = 3, Name = role.Admin }
                );


            //                                      User_Role relation
            modelBuilder.Entity<User_Role>().HasKey(ur => new { ur.User_Id, ur.Role_Id });

            modelBuilder.Entity<User_Role>().HasOne(ur => ur.users)
                                            .WithMany(u => u.User_Roles)
                                            .HasForeignKey(ur => ur.User_Id);

            modelBuilder.Entity<User_Role>().HasOne(ur => ur.roles)
                                            .WithMany(u => u.User_Roles)
                                            .HasForeignKey(ur => ur.Role_Id);


            //                                      Student relation with users and Depatment
            modelBuilder.Entity<Students>().HasKey(s => s.Id);

            modelBuilder.Entity<Students>().HasOne(s => s.User)
                                           .WithMany(u => u.students)
                                           .HasForeignKey(s=>s.User_Id);

            modelBuilder.Entity<Students>().HasOne(s => s.departments)
                                           .WithMany(d => d.students)
                                           .HasForeignKey(s => s.Depatment_Id);


            //                                       Instructor relation with users and Department 
            modelBuilder.Entity<Instructors>().HasKey(i => i.Id);

            modelBuilder.Entity<Instructors>().HasOne(i => i.User)
                                              .WithMany(u => u.instructors)
                                              .HasForeignKey(i => i.User_Id);

            modelBuilder.Entity<Instructors>().HasOne(i => i.departments)
                                              .WithMany(d => d.instructors)
                                              .HasForeignKey(i=>i.Department_Id);


            //                                       Teaches relation with Instructor and Section
            modelBuilder.Entity<Teaches>().HasKey(t => new { t.Instructor_Id, t.Section_Id });

            modelBuilder.Entity<Teaches>().HasOne(t => t.instructors)
                                          .WithMany(i => i.teaches)
                                          .HasForeignKey(t => t.Instructor_Id)
                                          .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Teaches>().HasOne(t => t.sections)
                                          .WithOne(i => i.teaches)
                                          .HasForeignKey<Teaches>(t => t.Section_Id)
                                          .OnDelete(DeleteBehavior.NoAction);



            //                                       Takes relation witn Students and Section
            modelBuilder.Entity<Takes>().HasKey(t => new { t.Student_Id, t.Section_Id });

            modelBuilder.Entity<Takes>().HasOne(t => t.students)
                                        .WithMany(s => s.takes)
                                        .HasForeignKey(t=>t.Student_Id)
                                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Takes>().HasOne(t => t.sections)
                                        .WithMany(s => s.takes)
                                        .HasForeignKey(t=>t.Section_Id)
                                        .OnDelete(DeleteBehavior.NoAction);


            //                                       Courses raletion with Department
            modelBuilder.Entity<Courses>().HasKey(c => c.Id);

            modelBuilder.Entity<Courses>().HasOne(c => c.departments)
                                          .WithMany(d => d.courses)
                                          .HasForeignKey(c=>c.Department_Id);


            //                                        Section releation with Courses,Classrooms and Time_Slote
            modelBuilder.Entity<Sections>().HasKey(s=>s.Id);

            modelBuilder.Entity<Sections>().HasOne(s => s.courses)
                                           .WithOne(c => c.section)
                                           .HasForeignKey<Sections>(s => s.Course_Id);

            modelBuilder.Entity<Sections>().HasOne(s => s.classrooms)
                                           .WithMany(c => c.sections)
                                           .HasForeignKey(s => s.Classroom_Id);

            modelBuilder.Entity<Sections>().HasOne(s => s.time_slots)
                                           .WithMany(c => c.sections)
                                           .HasForeignKey(s => s.Time_Slot_Id);


            ////////////////////////  default admin
            modelBuilder.Entity<Users>().HasData(new Users
            { Id = 10203040,UserId=10203040, First_name = "mananger", Last_name = "system", Email = "System@gmai", Hashed_password = "1234", Created_at = new DateTime(2000, 05, 01) });
            modelBuilder.Entity<User_Role>().HasData(new User_Role { User_Id = 10203040, Role_Id = 3 });
            ///////               default teacher
            modelBuilder.Entity<Users>().HasData(new Users
            { Id = 10, UserId = 10203050, First_name = "Teacher", Last_name = "T", Email = "@teach", Hashed_password = "1234", Created_at = new DateTime(2000, 05, 11) });
            modelBuilder.Entity<User_Role>().HasData(new User_Role { User_Id = 10, Role_Id = 2 });
            //                     default student
            modelBuilder.Entity<Users>().HasData(new Users
            { Id = 20, UserId = 10203060, First_name = "Student", Last_name = "S", Email = "@Styd", Hashed_password = "1234", Created_at = new DateTime(2000, 05, 21) });
            modelBuilder.Entity<User_Role>().HasData(new User_Role { User_Id = 20, Role_Id = 1 });


            //                                      defauld Department

            modelBuilder.Entity<Users>().HasData(new Users
            { Id = 40302010, First_name = "mananger1", Last_name = "system1", Email = "System1@gmai", Hashed_password = "4321", Created_at = new DateTime(2000, 05, 01) });
            modelBuilder.Entity<User_Role>().HasData(new User_Role { User_Id = 40302010, Role_Id = 2 });


            modelBuilder.Entity<Departments>().HasData(new Departments
            {
                Id = 111,
                Name = "کامپیوتر",
                Building = "0015",
                Budget = 50000000
            });


            modelBuilder.Entity<Departments>().HasData(new Departments
            {
                Id = 222,
                Name = "مکانیک",
                Building = "0154",
                Budget = 100000000
            });

            modelBuilder.Entity<Departments>().HasData(new Departments
            {
                Id = 333,
                Name = "برق",
                Building = "1023",
                Budget = 150000000
            });

            modelBuilder.Entity<Departments>().HasData(new Departments
            {
                Id = 444,
                Name = "معماری",
                Building = "4457",
                Budget = 25000000
            });

            //                                           default class

            modelBuilder.Entity<Classrooms>().HasData(new Classrooms
            {
                Id=11,
                Building= "کلاس 11",
                Room_Number=1,
                Capacity=20
            });
            modelBuilder.Entity<Classrooms>().HasData(new Classrooms
            {
                Id = 22,
                Building = "کلاس 22",
                Room_Number = 2,
                Capacity = 25
            });
            modelBuilder.Entity<Classrooms>().HasData(new Classrooms
            {
                Id = 33,
                Building = "کلاس 33",
                Room_Number = 3,
                Capacity = 15
            });
            modelBuilder.Entity<Classrooms>().HasData(new Classrooms
            {
                Id = 44,
                Building = "کلاس 44",
                Room_Number = 4,
                Capacity = 30
            });
            modelBuilder.Entity<Classrooms>().HasData(new Classrooms
            {
                Id = 55,
                Building = "کلاس 55",
                Room_Number = 5,
                Capacity = 50
            });

            //                                         default time

            modelBuilder.Entity<Time_Slots>().HasData(new Time_Slots
            {
                Id = 1,
                Day = "شنبه",
                Start_Time = new DateTime(1, 1, 1, 7, 30, 0),
                End_Time = new DateTime(1, 1, 1, 9, 0, 0)
            });

            modelBuilder.Entity<Time_Slots>().HasData(new Time_Slots
            {
                Id = 2,
                Day = "شنبه",
                Start_Time = new DateTime(1, 1, 1, 9, 0, 0),
                End_Time = new DateTime(1, 1, 1, 10, 30, 0)
            });

            modelBuilder.Entity<Time_Slots>().HasData(new Time_Slots
            {
                Id = 3,
                Day = "شنبه",
                Start_Time = new DateTime(1, 1, 1, 10, 30, 0),
                End_Time = new DateTime(1, 1, 1, 12, 0, 0)
            });

            modelBuilder.Entity<Time_Slots>().HasData(new Time_Slots
            {
                Id = 4,
                Day = "شنبه",
                Start_Time = new DateTime(1, 1, 1, 14, 30, 0),
                End_Time = new DateTime(1, 1, 1, 16, 0, 0)
            });

            modelBuilder.Entity<Time_Slots>().HasData(new Time_Slots
            {
                Id = 5,
                Day = "شنبه",
                Start_Time = new DateTime(1, 1, 1, 16, 0, 0),
                End_Time = new DateTime(1, 1, 1, 17, 30, 0)
            });
            //یکشنبه
            modelBuilder.Entity<Time_Slots>().HasData(new Time_Slots
            {
                Id = 6,
                Day = "یکشنبه",
                Start_Time = new DateTime(1, 1, 1, 7, 30, 0),
                End_Time = new DateTime(1, 1, 1, 9, 0, 0)
            });

            modelBuilder.Entity<Time_Slots>().HasData(new Time_Slots
            {
                Id = 7,
                Day = "یکشنبه",
                Start_Time = new DateTime(1, 1, 1, 9, 0, 0),
                End_Time = new DateTime(1, 1, 1, 10, 30, 0)
            });

            modelBuilder.Entity<Time_Slots>().HasData(new Time_Slots
            {
                Id = 8,
                Day = "یکشنبه",
                Start_Time = new DateTime(1, 1, 1, 10, 30, 0),
                End_Time = new DateTime(1, 1, 1, 12, 0, 0)
            });

            modelBuilder.Entity<Time_Slots>().HasData(new Time_Slots
            {
                Id = 9,
                Day = "یکشنبه",
                Start_Time = new DateTime(1, 1, 1, 14, 30, 0),
                End_Time = new DateTime(1, 1, 1, 16, 0, 0)
            });

            modelBuilder.Entity<Time_Slots>().HasData(new Time_Slots
            {
                Id = 10,
                Day = "یکشنبه",
                Start_Time = new DateTime(1, 1, 1, 16, 0, 0),
                End_Time = new DateTime(1, 1, 1, 17, 30, 0)
            });
            // دوشنبه
            modelBuilder.Entity<Time_Slots>().HasData(new Time_Slots
            {
                Id = 11,
                Day = "دوشنبه",
                Start_Time = new DateTime(1, 1, 1, 7, 30, 0),
                End_Time = new DateTime(1, 1, 1, 9, 0, 0)
            });

            modelBuilder.Entity<Time_Slots>().HasData(new Time_Slots
            {
                Id = 12,
                Day = "دوشنبه",
                Start_Time = new DateTime(1, 1, 1, 9, 0, 0),
                End_Time = new DateTime(1, 1, 1, 10, 30, 0)
            });

            modelBuilder.Entity<Time_Slots>().HasData(new Time_Slots
            {
                Id = 13,
                Day = "دوشنبه",
                Start_Time = new DateTime(1, 1, 1, 10, 30, 0),
                End_Time = new DateTime(1, 1, 1, 12, 0, 0)
            });

            modelBuilder.Entity<Time_Slots>().HasData(new Time_Slots
            {
                Id = 14,
                Day = "دوشنبه",
                Start_Time = new DateTime(1, 1, 1, 14, 30, 0),
                End_Time = new DateTime(1, 1, 1, 16, 0, 0)
            });

            modelBuilder.Entity<Time_Slots>().HasData(new Time_Slots
            {
                Id = 15,
                Day = "دوشنبه",
                Start_Time = new DateTime(1, 1, 1, 16, 0, 0),
                End_Time = new DateTime(1, 1, 1, 17, 30, 0)
            });
            // سه شنبه
            modelBuilder.Entity<Time_Slots>().HasData(new Time_Slots
            {
                Id = 16,
                Day = "سه شنبه",
                Start_Time = new DateTime(1, 1, 1, 7, 30, 0),
                End_Time = new DateTime(1, 1, 1, 9, 0, 0)
            });

            modelBuilder.Entity<Time_Slots>().HasData(new Time_Slots
            {
                Id = 17,
                Day = "سه شنبه",
                Start_Time = new DateTime(1, 1, 1, 9, 0, 0),
                End_Time = new DateTime(1, 1, 1, 10, 30, 0)
            });

            modelBuilder.Entity<Time_Slots>().HasData(new Time_Slots
            {
                Id = 18,
                Day = "سه شنبه",
                Start_Time = new DateTime(1, 1, 1, 10, 30, 0),
                End_Time = new DateTime(1, 1, 1, 12, 0, 0)
            });

            modelBuilder.Entity<Time_Slots>().HasData(new Time_Slots
            {
                Id = 19,
                Day = "سه شنبه",
                Start_Time = new DateTime(1, 1, 1, 14, 30, 0),
                End_Time = new DateTime(1, 1, 1, 16, 0, 0)
            });

            modelBuilder.Entity<Time_Slots>().HasData(new Time_Slots
            {
                Id = 20,
                Day = "سه شنبه",
                Start_Time = new DateTime(1, 1, 1, 16, 0, 0),
                End_Time = new DateTime(1, 1, 1, 17, 30, 0)
            });
            //چهارشنبه
            modelBuilder.Entity<Time_Slots>().HasData(new Time_Slots
            {
                Id = 21,
                Day = "چهارشنبه",
                Start_Time = new DateTime(1, 1, 1, 7, 30, 0),
                End_Time = new DateTime(1, 1, 1, 9, 0, 0)
            });

            modelBuilder.Entity<Time_Slots>().HasData(new Time_Slots
            {
                Id = 22,
                Day = "چهارشنبه",
                Start_Time = new DateTime(1, 1, 1, 9, 0, 0),
                End_Time = new DateTime(1, 1, 1, 10, 30, 0)
            });

            modelBuilder.Entity<Time_Slots>().HasData(new Time_Slots
            {
                Id = 23,
                Day = "چهارشنبه",
                Start_Time = new DateTime(1, 1, 1, 10, 30, 0),
                End_Time = new DateTime(1, 1, 1, 12, 0, 0)
            });

            modelBuilder.Entity<Time_Slots>().HasData(new Time_Slots
            {
                Id = 24,
                Day = "چهارشنبه",
                Start_Time = new DateTime(1, 1, 1, 14, 30, 0),
                End_Time = new DateTime(1, 1, 1, 16, 0, 0)
            });

            modelBuilder.Entity<Time_Slots>().HasData(new Time_Slots
            {
                Id =25,
                Day = "چهارشنبه",
                Start_Time = new DateTime(1, 1, 1, 16, 0, 0),
                End_Time = new DateTime(1, 1, 1, 17, 30, 0)
            });
            //پنجشنبه
            modelBuilder.Entity<Time_Slots>().HasData(new Time_Slots
            {
                Id = 26,
                Day = "پنجشنبه",
                Start_Time = new DateTime(1, 1, 1, 7, 30, 0),
                End_Time = new DateTime(1, 1, 1, 9, 0, 0)
            });

            modelBuilder.Entity<Time_Slots>().HasData(new Time_Slots
            {
                Id = 27,
                Day = "پنجشنبه",
                Start_Time = new DateTime(1, 1, 1, 9, 0, 0),
                End_Time = new DateTime(1, 1, 1, 10, 30, 0)
            });

            modelBuilder.Entity<Time_Slots>().HasData(new Time_Slots
            {
                Id = 28,
                Day = "پنجشنبه",
                Start_Time = new DateTime(1, 1, 1, 10, 30, 0),
                End_Time = new DateTime(1, 1, 1, 12, 0, 0)
            });

            modelBuilder.Entity<Time_Slots>().HasData(new Time_Slots
            {
                Id =29,
                Day = "پنجشنبه",
                Start_Time = new DateTime(1, 1, 1, 14, 30, 0),
                End_Time = new DateTime(1, 1, 1, 16, 0, 0)
            });

            modelBuilder.Entity<Time_Slots>().HasData(new Time_Slots
            {
                Id = 30,
                Day = "پنجشنبه",
                Start_Time = new DateTime(1, 1, 1, 16, 0, 0),
                End_Time = new DateTime(1, 1, 1, 17, 30, 0)
            });
        }
    }
}
