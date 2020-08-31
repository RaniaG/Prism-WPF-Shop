using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace E_Shop.Entities
{
    [XmlRoot("OrderItem")]
    public class CartItem
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
