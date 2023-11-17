using WebApplication2.Models;

namespace WebApplication2.Services
{
    public interface ICatalogService
    {
        Catalog GetCatalog();
        Catalog GetCatalogs(string catalogName);
    }
}