using Microsoft.EntityFrameworkCore;
using CMCS.Models;

namespace CMCS.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Claim> Claims { get; set; }
        public DbSet<SupportingDocs> SupportingDocs { get; set; }
    }
}
