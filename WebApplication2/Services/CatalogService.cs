using Microsoft.Extensions.Configuration;
using System.IO;
using System.Xml.Serialization;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly string _xmlFilePath;

        public CatalogService(IConfiguration configuration)
        {
            _xmlFilePath = configuration["XmlFilePath"];
        }
        public Catalog GetCatalogByName(string catalogName)
        {
            string xmlContent = File.ReadAllText(_xmlFilePath);

            XmlSerializer serializer = new XmlSerializer(typeof(Catalog));

            using (StringReader reader = new StringReader(xmlContent))
            {
                Catalog rootCatalog = (Catalog)serializer.Deserialize(reader);
                return FindCatalogByName(rootCatalog, catalogName);
            }
        }

        private Catalog FindCatalogByName(Catalog currentCatalog, string targetName)
        {
            if (currentCatalog.Name.Equals(targetName, StringComparison.OrdinalIgnoreCase))
            {
                return currentCatalog;
            }

            foreach (var subCatalog in currentCatalog.SubCatalogs)
            {
                Catalog foundCatalog = FindCatalogByName(subCatalog, targetName);
                if (foundCatalog != null)
                {
                    return foundCatalog;
                }
            }

            return null;
        }
        public Catalog GetCatalog()
        {
            if (File.Exists(_xmlFilePath))
            {
                string xmlContent = File.ReadAllText(_xmlFilePath);

                XmlSerializer serializer = new XmlSerializer(typeof(Catalog));

                using (StringReader reader = new StringReader(xmlContent))
                {
                    Catalog catalog = (Catalog)serializer.Deserialize(reader);
                    return catalog;
                }
            }
            else
            {
                // Обробка випадку, коли файл не існує
                return null;
            }
        }
    }
}
