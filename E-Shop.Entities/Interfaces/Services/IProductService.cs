﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Entities.Interfaces.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll(Func<Product,bool> predicate);
        Product GetProduct(int id);
    }
}
