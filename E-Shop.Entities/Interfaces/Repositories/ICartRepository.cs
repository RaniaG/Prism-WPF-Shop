using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Entities.Interfaces.Repositories
{
    public interface ICartRepository
    {
        IEnumerable<CartItem> GetUserCart(int userId);
        int GetUserCartItemsCount(int userId);
        void SaveCartItems(IEnumerable<CartItem> cartItems);
    }
}
