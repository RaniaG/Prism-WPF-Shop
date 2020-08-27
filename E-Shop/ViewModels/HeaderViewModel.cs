using E_Shop.Core.Consts;
using E_Shop.Core.Entities;
using E_Shop.Core.Events;
using Prism.Events;
using Prism.Mvvm;
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
        private readonly IEventAggregator _eventAggregator;
        public HeaderViewModel(IEventAggregator eventAggregator)
        {
            Username = "Rania";
            IsFilterVisible = true;

            _eventAggregator = eventAggregator;

            SubscribeToEvents();
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

        private void UpdateCartItems(CartEventItem cartEventItem)
        {
            switch (cartEventItem.CartAction)
            {
                case CartAction.Add:
                    CartItemsCount += cartEventItem.Count;
                    break;
                case CartAction.Remove:
                    CartItemsCount -= cartEventItem.Count;
                    break;
            }
        }


    }
}
