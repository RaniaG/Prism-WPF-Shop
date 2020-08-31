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

        private IEnumerable<CartItem> _cartItems;

        public CartRepository(IXMLReader xMLReader,IXMLWriter xMLWriter)
        {
            _xMLReader = xMLReader;
            _xMLWriter = xMLWriter;
            ReadAll();
        }
        private void ReadAll()
        {
            _cartItems= _xMLReader.Read<CartItem>(DataFilesPaths.Cart);
        }
        public IEnumerable<CartItem> GetUserCart(int userId)
        {
           return _cartItems.Where(e => e.UserId == userId);
        }

        public void SaveCartItems(IEnumerable<CartItem> cartItems)
        {
            foreach (var item in cartItems)
            {
                var savedItem=_cartItems.FirstOrDefault(e => e.ProductId == item.ProductId && e.UserId == item.UserId);
                if(savedItem==null)
                {
                    _cartItems.Append(item);
                }
                else
                {
                    savedItem.Count = item.Count;
                }
            }
            _xMLWriter.Write(DataFilesPaths.Cart,"Cart", _cartItems);
        }

        public int GetUserCartItemsCount(int userId)
        {
            var result = GetUserCart(userId);
            var sum = 0;
            if (result != null && result.Any())
            {
                foreach (var item in result)
                {
                    sum += item.Count;
                }
            }
            return sum;
        }
    }
}
