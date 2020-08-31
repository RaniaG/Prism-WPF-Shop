using E_Shop.DAL.Consts;
using E_Shop.DAL.XMLReader;
using E_Shop.Entities;
using E_Shop.Entities.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IXMLReader _xMLReader;
        private IEnumerable<Product> _products;
        public ProductRepository(IXMLReader xMLReader)
        {
            _xMLReader = xMLReader;
            ReadAll();
        }
        private void ReadAll()
        {
            _products = _xMLReader.Read<Product>(DataFilesPaths.Products);
        }
        public IEnumerable<Product> GetAll(Func<Product, bool> predicate)
        {
            return _products.Where(predicate);
        }

        public Product GetProduct(int id)
        {
            return _products.FirstOrDefault(e => e.Id == id);
        }
    }
}
