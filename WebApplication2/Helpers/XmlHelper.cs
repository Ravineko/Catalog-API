using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using WebApplication2.Models;

namespace WebApplication2.Helpers
{
    public static class XmlHelper
    {
        public static void SaveToXml(Catalog catalog, string filePath)
        {
            var serializer = new XmlSerializer(typeof(Catalog));

            using (var stream = new StreamWriter(filePath))
            {
                serializer.Serialize(stream, catalog);
            }
        }

        public static Catalog LoadFromXml(string filePath)
        {
            if (File.Exists(filePath))
            {
                var serializer = new XmlSerializer(typeof(Catalog));

                using (var stream = new StreamReader(filePath))
                {
                    return (Catalog)serializer.Deserialize(stream);
                }
            }

            return null;
        }
    }
}
