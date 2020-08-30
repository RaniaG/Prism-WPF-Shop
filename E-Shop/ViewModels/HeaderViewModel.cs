using E_Shop.Core.Consts;
using E_Shop.Core.Entities;
using E_Shop.Core.Events;

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

        public HeaderViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            Username = "Rania";
            IsFilterVisible = true;

            _eventAggregator = eventAggregator;
            _regionManager = regionManager;

            SubscribeToEvents();
            InitCommands();
        }
        private void SubscribeToEvents()
        {
            //Show Filter Event
            var ShowFilterEvent = _eventAggregator.GetEvent<ShowFilterEvent>();
            ShowFilterEvent.Subscribe((res) => IsFilterVisible = res);

            //Update Cart Event
            var UpdateCartEvent = _eventAggregator.GetEvent<AddToCartEvent>();
            UpdateCartEvent.Subscribe((res) => UpdateCartItems(res));
        }
        private void InitCommands()
        {
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void UpdateCartItems(CartItem cartEventItem)
        {
            CartItemsCount += cartEventItem.Count;
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
