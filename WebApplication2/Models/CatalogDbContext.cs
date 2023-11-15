using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }

        public DbSet<Catalog> Catalogs { get; set; }
    }
}
