using E_Shop.Core.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Models
{
    public class CartItemEventModel:CartItemModel
    {
        public CartAction Action { get; set; }
    }
}
