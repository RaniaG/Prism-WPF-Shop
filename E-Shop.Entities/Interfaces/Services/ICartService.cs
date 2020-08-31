using E_Shop.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Entities.Interfaces.Services
{
    public interface ICartService
    {
        IEnumerable<CartItemDto> GetUserCart(int userId);
        void AddToCart( CartItem cartItem);
        int GetUserCartItemsCount(int userId);
        void RemoveFromCart( CartItem cartItem);
        void SubmitCart();
    }
}
