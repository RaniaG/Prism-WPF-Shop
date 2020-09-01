using E_Shop.DAL.Consts;
using E_Shop.Entities;
using E_Shop.Entities.Interfaces.Repositories;
using E_Shop.Entities.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Business.Services
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> GetAll(Func<Product, bool> predicate)
        {
            return _productRepository.GetAll(predicate).OrderBy(e => e.Price);
        }

        public Product GetProduct(int id)
        {
            return _productRepository.GetProduct(id);
        }
    }
}
