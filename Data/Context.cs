using Microsoft.EntityFrameworkCore;
using CMCS.Models;
using CMCS.Data;

namespace CMCS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Claim> SupportingDocs { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
