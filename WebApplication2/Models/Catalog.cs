using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace WebApplication2.Models {
    [XmlRoot("Catalog")]
    public class Catalog
    {
        public int CatalogId { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlElement("Subcatalogs")]
        public List<Catalog> SubCatalogs { get; set; }

    }

}