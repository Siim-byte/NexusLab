using Microsoft.EntityFrameworkCore;
using Nexus.Core.Domain;
using Nexus.Nexus.Core.Domain;
namespace Nexus.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
