using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace WebApplication2.Models {
    [XmlRoot("Catalog")]
    public class Catalog
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlElement("Subcatalogs")]
        public List<Catalog> SubCatalogs { get; set; }

        //[XmlElement("Catalogs")]
        //public List<Catalog> ?ChildCatalogs { get; set; }
    }

}