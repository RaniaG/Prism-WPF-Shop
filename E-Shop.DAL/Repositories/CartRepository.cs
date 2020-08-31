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
    public class CartRepository : ICartRepository
    {
        private readonly IXMLReader _xMLReader;
        private readonly IXMLWriter _xMLWriter;

        public CartRepository(IXMLReader xMLReader,IXMLWriter xMLWriter)
        {
            _xMLReader = xMLReader;
            _xMLWriter = xMLWriter;
        }

        public void SaveCartItems(IEnumerable<CartItem> cartItems)
        {
            _xMLWriter.Append(DataFilesPaths.Orders, cartItems, "Order");
        }

    }
}
