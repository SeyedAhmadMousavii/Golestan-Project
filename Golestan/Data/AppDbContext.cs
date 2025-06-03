using Golestan.Models;
using Microsoft.EntityFrameworkCore;

namespace Golestan.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<User_Role> User_Roles { get; set; }
        public DbSet<Students> Students { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            /////////////////  User_Role relation
            modelBuilder.Entity<User_Role>().HasKey(ur => new { ur.User_Id, ur.Role_Id });

            modelBuilder.Entity<User_Role>().HasOne(ur => ur.users)
                                            .WithMany(u => u.User_Roles)
                                            .HasForeignKey(ur => ur.User_Id);

            modelBuilder.Entity<User_Role>().HasOne(ur => ur.roles)
                                            .WithMany(u => u.User_Roles)
                                            .HasForeignKey(ur => ur.Role_Id);
            ////////////////  Role cunstructor
            modelBuilder.Entity<Roles>().HasData
                (
                new Roles { Id = 1, Name = role.Student },
                new Roles { Id = 2, Name = role.Teacher },
                new Roles { Id = 3, Name = role.Admin }
                );
            ////////////////////////set relation between user and student
            modelBuilder.Entity<Students>().HasOne(s => s.users)
                                          .WithMany(u => u.students)
                                          .HasForeignKey(s => s.User_Id);
            ////////////////////////  default admin
            modelBuilder.Entity<Users>().HasData(new Users
            { Id = 10203040, First_name = "mananger", Last_name = "system", Email = "System@gmai", Hashed_password = "1234", Created_at = new DateTime(2000, 05, 01) });
            modelBuilder.Entity<User_Role>().HasData(new User_Role { User_Id = 10203040, Role_Id = 3 });
        }
    }
}
