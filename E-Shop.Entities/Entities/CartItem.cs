using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Entities
{
    public class CartItem
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
