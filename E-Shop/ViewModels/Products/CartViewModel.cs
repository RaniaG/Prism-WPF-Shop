using E_Shop.Consts;
using E_Shop.Core.Consts;
using E_Shop.Core.Events;
using E_Shop.Dialogs;
using E_Shop.Entities;
using E_Shop.Entities.Interfaces.Services;
using E_Shop.Events;
using E_Shop.Models;
using E_Shop.Services;
using E_Shop.Views.Products;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
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
        private readonly IDialogService _dialogService;
        private readonly IMessageResourceManager _messageResourceManager;



        public CartViewModel(IRegionManager regionManager,IEventAggregator eventAggregator,
            IUserService userService,ICartService cartService ,IDialogService dialogService,
            IMessageResourceManager messageResourceManager)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _cartService = cartService;
            _userService = userService;
            _dialogService = dialogService;
            _messageResourceManager = messageResourceManager;

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
                    Product = new ProductModel { Id = e.Product.Id, Title = e.Product.Title, Description = e.Product.Description, Price = e.Product.Price*e.Count, InStock = e.Product.InStock, ImageUrl = e.Product.ImageUrl, Images=e.Product.Images }
                };
            }));
            CartNotEmpty = userCart.Any();
        }

        private void InitCommands()
        {
            SubmitCommand = new DelegateCommand(Submit, CanSubmit);
            SubmitCommand.ObservesProperty(() => CartNotEmpty);
            DeleteCommand = new DelegateCommand<CartItemModel>(Delete);

        }

        private bool CanSubmit()
        {
            return Products!=null&&Products.Count > 0 && Products.FirstOrDefault(e => !e.Product.InStock) == null ;
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
            SubmitCommand.RaiseCanExecuteChanged();
        }

        private void Submit()
        {
            _cartService.SubmitCart();
            var eventPayload = new CartItemEventModel
            {
                Action = CartAction.Submit
            };
            _eventAggregator.GetEvent<UpdateCartEvent>().Publish(eventPayload);
            _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(ProductsListView));

            var dialogParams = new DialogParameters();
            dialogParams.Add("Message", _messageResourceManager.GetMessage(Messages.OrderSubmitSuccess));
            dialogParams.Add("Type", MessageDialogType.Success);
            _dialogService.ShowDialog(nameof(MessageDialogView), dialogParams, (res) => { });
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
