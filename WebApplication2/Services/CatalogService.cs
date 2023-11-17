using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly CatalogDbContext _dbContext;

        public CatalogService(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
        }

        public Catalog GetCatalog()
        {
            return _dbContext.Catalogs.Include(c => c.SubCatalogs).FirstOrDefault();
        }

        public Catalog GetCatalogs(string catalogName)
        {
            var catalog = _dbContext.Catalogs
                .Include(c => c.SubCatalogs)
                .FirstOrDefault(c => c.Name.Equals(catalogName, StringComparison.OrdinalIgnoreCase));

            return catalog;
        }
    }
}
