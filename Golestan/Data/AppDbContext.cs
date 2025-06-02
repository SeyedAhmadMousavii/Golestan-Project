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

    }
}
