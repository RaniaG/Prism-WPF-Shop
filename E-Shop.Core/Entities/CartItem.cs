using E_Shop.Core.Consts;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Core.Entities
{
    public class CartItem:BindableBase
    {
        public int Count { get; set; }
        public Product Product { get; set; }
    }
}
