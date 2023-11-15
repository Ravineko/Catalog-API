using Microsoft.AspNetCore.Mvc;
using WebApplication2.Services;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public IActionResult Index()
        {
            Catalog rootCatalog = _catalogService.GetCatalog();
            return View(rootCatalog);
        }

        [HttpGet]
        [Route("Catalog/{catalogName}")]
        public IActionResult _CatalogItem(string catalogName)
        {
            Catalog currentCatalog = _catalogService.GetCatalogByName(catalogName);

            if (currentCatalog == null)
            {
                // Обробка випадку, коли каталог не знайдено
                return NotFound();
            }

            return View(currentCatalog);
        }

        [HttpPost]
        [Route("Catalog/ChangeCurrentCatalog")]
        public IActionResult ChangeCurrentCatalog(string catalogName)
        {
            Catalog newCurrentCatalog = _catalogService.GetCatalogByName(catalogName);

            if (newCurrentCatalog == null)
            {
                // Обробка випадку, коли каталог не знайдено
                return NotFound();
            }

            // Поверніть частковий вигляд з новим поточним каталогом
            return PartialView("_CatalogItem", newCurrentCatalog);
        }
    }
}
