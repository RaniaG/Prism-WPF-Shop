using E_Shop.Core.Consts;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Models
{
    public class CartItemModel:BindableBase
    {
        private int _count=1;
        public int Count
        {
            get { return _count; }
            set { SetProperty(ref _count, value); }
        }
        private ProductModel _product;
        public ProductModel Product
        {
            get { return _product; }
            set { SetProperty(ref _product, value); }
        }
    }
}
