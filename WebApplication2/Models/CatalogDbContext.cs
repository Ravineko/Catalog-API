using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Catalog>()
        .HasKey(c => c.CatalogId);

        }
        public DbSet<Catalog> Catalogs { get; set; }
        public void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}
