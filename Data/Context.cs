/*using Microsoft.EntityFrameworkCore;
using CMCS.Models;
using CMCS.Data;

namespace CMCS.Data
{
    public class ApplicationDbContext : DbContext
    { 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Lecturer> Lecturer { get; set; }
        public DbSet<Claim> Claims { get; set; } 
        public DbSet<Manager> Manager { get; set; }
        public DbSet<User> User { get; set; }
    }
}
*/