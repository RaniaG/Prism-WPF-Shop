using E_Shop.Core.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Core.Entities
{
    public class CartEventItem : CartItem
    {
        public CartAction CartAction { get; set; }
    }
}
