using E_Shop.Core.Consts;
using E_Shop.Core.Events;
using E_Shop.Entities;
using E_Shop.Entities.Interfaces.Services;
using E_Shop.Models;
using E_Shop.Views.Products;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.ViewModels.Products
{
    public class CartViewModel : BindableBase,INavigationAware
    {
        private ObservableCollection<CartItemModel> _products;
        public ObservableCollection<CartItemModel> Products
        {
            get { return _products; }
            set
            {
                SetProperty(ref _products, value);
            }
        }

        private bool _cartNotEmpty;
        public bool CartNotEmpty
        {
            get { return _cartNotEmpty; }
            set { SetProperty(ref _cartNotEmpty, value); }
        }

        private int _userId;

        public DelegateCommand SubmitCommand { get; set; }
        public DelegateCommand<CartItemModel> DeleteCommand { get; set; }
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IUserService _userService;
        private readonly ICartService _cartService;

        public CartViewModel(IRegionManager regionManager,IEventAggregator eventAggregator,
            IUserService userService,ICartService cartService )
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _cartService = cartService;
            _userService = userService;

            InitCommands();
        }

        private void InitData()
        {
            _userId = _userService.GetCurrentUser().Id;
            var userCart = _cartService.GetUserCart(_userId);
            Products = new ObservableCollection<CartItemModel>(userCart.Select(e =>
            {
                return new CartItemModel
                {
                    Count = e.Count,
                    Product = new ProductModel { Id = e.Product.Id, Title = e.Product.Title, Description = e.Product.Description, Price = e.Product.Price*e.Count, InStock = e.Product.InStock, ImageUrl = e.Product.ImageUrl }
                };
            }));
            CartNotEmpty = userCart.Any();
        }

        private void InitCommands()
        {
            SubmitCommand = new DelegateCommand(Submit, CanSubmit);
            SubmitCommand.ObservesCanExecute(() => CartNotEmpty);
            DeleteCommand = new DelegateCommand<CartItemModel>(Delete);

        }

        private bool CanSubmit()
        {
            return Products.Count > 0;
        }

        private void Delete(CartItemModel obj)
        {
            _cartService.RemoveFromCart(new CartItem { UserId = _userId, ProductId = obj.Product.Id });
            Products.Remove(obj);
            if (Products.Count == 0)
                CartNotEmpty = false;
            else CartNotEmpty = true;

            var eventPayload = new CartItemEventModel
            {
                Product = obj.Product,
                Count = obj.Count,
                Action = CartAction.Remove
            };
            _eventAggregator.GetEvent<UpdateCartEvent>().Publish(eventPayload);
        }

        private void Submit()
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(ProductsListView));
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            InitData();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
