using E_Shop.Core.Consts;
using E_Shop.Core.Events;
using E_Shop.Entities.Interfaces.Services;
using E_Shop.Models;
using E_Shop.Views;
using E_Shop.Views.Products;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.ViewModels
{
    public class HeaderViewModel : BindableBase
    {
        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }
        private int _cartItemsCount;
        public int CartItemsCount
        {
            get { return _cartItemsCount; }
            set { SetProperty(ref _cartItemsCount, value); }
        }
        private bool _isFilterVisible;
        public bool IsFilterVisible
        {
            get { return _isFilterVisible; }
            set { SetProperty(ref _isFilterVisible, value); }
        }

        public DelegateCommand<string> NavigateCommand { get; set; }

        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        private readonly ICartService _cartService;
        private readonly IUserService _userService;



        public HeaderViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, ICartService cartService,
            IUserService userService)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _cartService = cartService;
            _userService = userService;

            SubscribeToEvents();
            InitCommands();
            InitData();
        }

        private void InitData()
        {
            var user = _userService.GetCurrentUser();
            Username = user.Name;
            CartItemsCount = _cartService.GetUserCartItemsCount(user.Id);
            IsFilterVisible = true;
        }

        private void SubscribeToEvents()
        {
            //Show Filter Event
            var ShowFilterEvent = _eventAggregator.GetEvent<ShowFilterEvent>();
            ShowFilterEvent.Subscribe((res) => IsFilterVisible = res);

            //Update Cart Event
            var UpdateCartEvent = _eventAggregator.GetEvent<UpdateCartEvent>();
            UpdateCartEvent.Subscribe((res) => UpdateCartItems(res));
        }
        private void InitCommands()
        {
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void UpdateCartItems(CartItemEventModel cartEventItem)
        {
            switch (cartEventItem.Action)
            {
                case CartAction.Add:
                    CartItemsCount += cartEventItem.Count;
                    break;
                case CartAction.Remove:
                    CartItemsCount -= cartEventItem.Count;
                    break;
            }
        }
        private void Navigate(string url)
        {
            switch (url)
            {
                case "home":
                    _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(ProductsListView));
                    break;
                case "cart":
                    _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(CartView));
                    break;
                case "logout":
                    _regionManager.RequestNavigate(RegionNames.MainRegion, nameof(LoginView));
                    break;
                case "filter":
                    _eventAggregator.GetEvent<ShowFilterDialogEvent>().Publish();
                    break;

            }
        }


    }
}
