using E_Shop.Core.Consts;
using E_Shop.Core.Entities;
using E_Shop.Core.Events;
using E_Shop.Products.Views;
using Prism;
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

namespace E_Shop.Products.ViewModels
{
    public class ProductsListViewModel:BindableBase,INavigationAware
    {
        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set { SetProperty(ref _products, value); }
        }
        public DelegateCommand<Product> NavigateToProductCommand { get; set; }

        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;


        public ProductsListViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            Products = new ObservableCollection<Product>(new List<Product>
            {
                new Product { Id=1, Title="LG Smart TV", Description="lsdfsdfsssssssssssssssssssssdfsdfsdfsdfsddddddddddddddddddddddddddddddddddddddddddddddddddd" , InStock=true, Price=50, ImageUrl="../../Assets/Products/tv.jpg"},
                new Product { Id=2, Title="LG Smart TV 2", Description="lsdfsdfsssssssssssssssssssssdfsdfsdfsdfsddddddddddddddddddddddddddddddddddddddddddddddddddd" , InStock=true, Price=50, ImageUrl="../../Assets/Products/tv.jpg"},
                new Product { Id=3, Title="LG Smart TV 3", Description="lsdfsdfsssssssssssssssssssssdfsdfsdfsdfsddddddddddddddddddddddddddddddddddddddddddddddddddd" , InStock=true, Price=50, ImageUrl="../../Assets/Products/tv.jpg"},
            });
            NavigateToProductCommand = new DelegateCommand<Product>(NavigateToProduct);
        }

        private void NavigateToProduct(Product product)
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("product", product);
            _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(ProductDetailsView), navigationParams);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _eventAggregator.GetEvent<ShowFilterEvent>().Publish(true);

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            _eventAggregator.GetEvent<ShowFilterEvent>().Publish(false);

        }
    }
}
