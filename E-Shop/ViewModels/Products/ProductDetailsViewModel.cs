﻿using E_Shop.Core.Consts;
using E_Shop.Core.Events;
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

        public ProductDetailsViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            InitCommands();
        }
        private void InitCommands()
        {
            UpdateCountCommand = new DelegateCommand<string>(UpdateCount);
            AddToCartCommand = new DelegateCommand(AddToCart);
        }
        private void AddToCart()
        {
            var eventPayload = new CartItemModel
            {
                Product = ProductCartItem.Product,
                Count = ProductCartItem.Count
            };
            _eventAggregator.GetEvent<AddToCartEvent>().Publish(eventPayload);
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
