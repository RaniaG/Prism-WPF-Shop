using E_Shop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace E_Shop.DAL.Entities
{
    [XmlRoot("Products")]
    public class ProductsXml
    {
        [XmlElement("Product")]
        public List<Product> Products { get; set; }
    }
}
