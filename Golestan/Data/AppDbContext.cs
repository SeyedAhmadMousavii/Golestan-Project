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
                                        .WithOne(s => s.takes)
                                        .HasForeignKey<Takes>(t=>t.Section_Id)
                                        .OnDelete(DeleteBehavior.NoAction);


            //                                       Courses raletion with Department
            modelBuilder.Entity<Courses>().HasKey(c => new { c.Department_Id });

            modelBuilder.Entity<Courses>().HasOne(c => c.departments)
                                          .WithMany(d => d.courses)
                                          .HasForeignKey(c=>c.Department_Id);


            //                                        Section releation with Courses,Classrooms and Time_Slote
            modelBuilder.Entity<Sections>().HasKey(s=>s.Id);

            modelBuilder.Entity<Sections>().HasOne(s => s.courses)
                                           .WithOne(c => c.sections)
                                           .HasForeignKey<Sections>(s=>s.Course_Id);

            modelBuilder.Entity<Sections>().HasOne(s => s.classrooms)
                                           .WithMany(c => c.sections)
                                           .HasForeignKey(s => s.Classroom_Id);

            modelBuilder.Entity<Sections>().HasOne(s => s.time_slots)
                                           .WithMany(c => c.sections)
                                           .HasForeignKey(s => s.Time_Slot_Id);


            ////////////////////////  default admin
            modelBuilder.Entity<Users>().HasData(new Users
            { Id = 10203040, First_name = "mananger", Last_name = "system", Email = "System@gmai", Hashed_password = "1234", Created_at = new DateTime(2000, 05, 01) });
            modelBuilder.Entity<User_Role>().HasData(new User_Role { User_Id = 10203040, Role_Id = 3 });

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

        }
    }
}
