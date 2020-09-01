using E_Shop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace E_Shop.DAL.Entities
{
    [XmlRoot("Orders")]
    public class OrdersXml
    {
        [XmlElement("Order")]
        public List<Order> Orders { get; set; }
    }
    public class Order
    {
        [XmlElement("OrderItem")]
        public List<CartItem> OrderItems { get; set; }
    }
}
