using Microsoft.EntityFrameworkCore;
using Nexus.Nexus.Core.Domain;

namespace Nexus.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
