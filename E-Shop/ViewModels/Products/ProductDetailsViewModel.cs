using E_Shop.Core.Consts;
using E_Shop.Core.Events;
using E_Shop.Entities.Interfaces.Services;
using E_Shop.Models;
using E_Shop.Views.Products;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.ViewModels.Products
{
    public class ProductDetailsViewModel : BindableBase, INavigationAware
    {
        private CartItemModel _productCartItem;
        public CartItemModel ProductCartItem
        {
            get { return _productCartItem; }
            set { SetProperty(ref _productCartItem, value); }
        }
        public DelegateCommand<string> UpdateCountCommand { get; set; }
        public DelegateCommand AddToCartCommand { get; set; }


        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        private readonly IUserService _userService;
        private readonly ICartService _cartService;


        public ProductDetailsViewModel(IEventAggregator eventAggregator, IRegionManager regionManager,
            IUserService userService,ICartService cartService)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _userService = userService;
            _cartService = cartService;
            InitCommands();
        }
        private void InitCommands()
        {
            UpdateCountCommand = new DelegateCommand<string>(UpdateCount);
            AddToCartCommand = new DelegateCommand(AddToCart);
        }
        private void AddToCart()
        {
            var userId = _userService.GetCurrentUser().Id;
            _cartService.AddToCart(new Entities.CartItem { Count = ProductCartItem.Count, ProductId = ProductCartItem.Product.Id, UserId = userId });
            var eventPayload = new CartItemEventModel
            {
                Product = ProductCartItem.Product,
                Count = ProductCartItem.Count,
                Action=CartAction.Add
            };
            _eventAggregator.GetEvent<UpdateCartEvent>().Publish(eventPayload);
            _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(ProductsListView));
        }

        private void UpdateCount(string obj)
        {
            switch (obj)
            {
                case "Inc":
                    ProductCartItem.Count++;
                    break;
                case "Dec":
                    if (ProductCartItem.Count >1)
                        ProductCartItem.Count--;
                    break;
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            ProductCartItem = new CartItemModel()
            {
                Product = navigationContext.Parameters.GetValue<ProductModel>("ProductModel")
            };
        }
    }
}
