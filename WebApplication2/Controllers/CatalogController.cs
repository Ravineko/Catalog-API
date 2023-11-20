using Microsoft.AspNetCore.Mvc;
using WebApplication2.Services;
using WebApplication2.Models;
using System.Xml.Serialization;

namespace WebApplication2.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly CatalogDbContext _dbContext;

        public CatalogController(ICatalogService catalogService, CatalogDbContext dbContext)
        {
            _catalogService = catalogService;
            _dbContext = dbContext;

            if (!_dbContext.Catalogs.Any())
            {
                // Ініціалізація даних в оперативній пам'яті або з іншого джерела, якщо база даних порожня
                var creatingDigitalImages = new Catalog { Name = "Creating Digital Images" };
                var resources = new Catalog { Name = "Resources" };
                var primarySources = new Catalog { Name = "Primary sources" };
                var secondarySources = new Catalog { Name = "Secondary sources" };
                var evidence = new Catalog { Name = "Evidence" };
                var graphicProducts = new Catalog { Name = "Graphic Products" };
                var process = new Catalog { Name = "Process" };
                var finalProduct = new Catalog { Name = "Final product" };

                // Додаємо каталоги до контексту бази даних
                _dbContext.Catalogs.AddRange(
                    creatingDigitalImages,
                    resources,
                    primarySources,
                    secondarySources,
                    evidence,
                    graphicProducts,
                    process,
                    finalProduct
                );

                // Додаємо підкаталоги до відповідних каталогів
                creatingDigitalImages.SubCatalogs = new List<Catalog> { resources, evidence, graphicProducts };
                graphicProducts.SubCatalogs = new List<Catalog> { process, finalProduct };
                resources.SubCatalogs = new List<Catalog> { primarySources, secondarySources };


                // Зберігаємо зміни в базі даних
                _dbContext.SaveChanges();
            }

        }
        public IActionResult Index()
        {
            Catalog catalogs = _catalogService.GetCatalog();
            return View(catalogs);
        }


        [HttpPost]
        public IActionResult ExportToXml()
        {
            if (!_dbContext.Catalogs.Any())
            {
                TempData["Message"] = "База даних порожня. Немає даних для експорту.";
                return RedirectToAction("Index");
            }

            var catalogs = _dbContext.Catalogs.ToList();
            var serializer = new XmlSerializer(typeof(List<Catalog>));
            using (var writer = new StreamWriter("XmlFiles/exportedCatalog.xml"))
            {
                serializer.Serialize(writer, catalogs);
            }

            TempData["Message"] = "Експорт успішно завершено.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ImportFromXml()
        {

            var serializer = new XmlSerializer(typeof(List<Catalog>));

            try
            {
                using (var reader = new StreamReader("XmlFiles/importedCatalog.xml"))
                {
                    var importedCatalogs = (List<Catalog>)serializer.Deserialize(reader);
                    _dbContext.Catalogs.AddRange(importedCatalogs);
                    _dbContext.SaveChanges();

                    TempData["Message"] = "Імпорт успішно завершено.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Помилка імпорту: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        //public IActionResult ClearDatabase()
        //{
        //    try
        //    {
        //        // Перевірка чи база даних не порожня
        //        if (_dbContext.Catalogs.Any())
        //        {
        //            // Видалення всіх каталогів
        //            _dbContext.Catalogs.RemoveRange(_dbContext.Catalogs);
        //            _dbContext.SaveChanges();


        //            TempData["Message"] = "Базу даних успішно очищено.";
        //        }
        //        else
        //        {
        //            TempData["Message"] = "База даних вже порожня.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["ErrorMessage"] = $"Помилка при очищенні бази даних: {ex.Message}";
        //    }

        //    // Перенаправлення на Index для оновлення виводу
        //    return RedirectToAction("Index");
        //}


        [HttpGet]
        [Route("Catalog/{catalogName}")]
        public IActionResult _CatalogItem(int catalogId)
        {

            Catalog catalogs = _catalogService.GetCatalogs(catalogId);

            if (catalogs == null)
            {
  
                return NotFound();
            }

            return View(catalogs);
        }

        [HttpPost]
        [Route("Catalog/ChangeCurrentCatalog")]
        public IActionResult ChangeCurrentCatalog(int catalogId)
        {
            Catalog catalogs = _catalogService.GetCatalogs(catalogId);

            if (catalogs == null)
            {
                return NotFound();
            }

            return PartialView("_CatalogItem", catalogs);
        }
    }
}
