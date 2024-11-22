using CMCS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CMCS.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Lecturer> Lecturer { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Manager> Manager { get; set; }
        public DbSet<User> User { get; set; }

        // Add any additional DbSet properties for your entities here, for example:
        // public DbSet<Claim> Claims { get; set; }
    }
}