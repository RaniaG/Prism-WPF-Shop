using E_Shop.Entities;
using E_Shop.Entities.DTOs;
using E_Shop.Entities.Interfaces.Repositories;
using E_Shop.Entities.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Business.Services
{
    public class CartService : ICartService
    {
        private List<CartItem> _cart;
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartService(ICartRepository cartRepository,IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _cart = new List<CartItem>();
        }
        public void AddToCart(CartItem cartItem)
        {
           var savedCartItem= _cart.FirstOrDefault(e => e.ProductId == cartItem.ProductId && e.UserId == cartItem.UserId);
            if(savedCartItem==null)
            {
                _cart.Add(savedCartItem);
            }
            else
            {
                savedCartItem.Count += cartItem.Count;
            }
        }

        public IEnumerable<CartItemDto> GetUserCart(int userId)
        {
            _cart = _cartRepository.GetUserCart(userId).ToList();
            return _cart.Select(e =>
            {
                return new CartItemDto
                {
                    Product = _productRepository.GetProduct(e.ProductId),
                    Count = e.Count
                };
            });
        }

        public void RemoveFromCart(CartItem cartItem)
        {
            var savedCartItem = _cart.FirstOrDefault(e => e.UserId == cartItem.UserId && e.ProductId == cartItem.ProductId);
            if(savedCartItem!=null)
                _cart.Remove(savedCartItem);
        }

        public void SubmitCart()
        {
            _cartRepository.SaveCartItems(_cart);
        }
    }
}
