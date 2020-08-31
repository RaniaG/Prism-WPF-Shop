using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Entities.DTOs
{
    public class CartItemDto
    {
        public int Count { get; set; }
        public Product Product { get; set; }
    }
}
