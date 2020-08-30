using E_Shop.Core.Consts;
using E_Shop.Core.Entities;
using E_Shop.Core.Events;
using E_Shop.Dialogs;
using E_Shop.Views.Products;
using Prism;
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
    public class ProductsListViewModel:BindableBase,INavigationAware
    {
        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set { SetProperty(ref _products, value); }
        }
        private ProductsFilter _productsFilter;
        public ProductsFilter ProductsFilter
        {
            get { return _productsFilter; }
            set { SetProperty(ref _productsFilter, value); }
        }

        public DelegateCommand<Product> NavigateToProductCommand { get; set; }

        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;



        public ProductsListViewModel(IEventAggregator eventAggregator, IRegionManager regionManager,IDialogService dialogService)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _dialogService = dialogService;

            Products = new ObservableCollection<Product>(new List<Product>
            {
                new Product { Id=1, Title="LG Smart TV", Description="lsdfsdfsssssssssssssssssssssdfsdfsdfsdfsddddddddddddddddddddddddddddddddddddddddddddddddddd" , InStock=true, Price=50, ImageUrl="../../Assets/Products/tv.jpg"},
                new Product { Id=2, Title="LG Smart TV 2", Description="lsdfsdfsssssssssssssssssssssdfsdfsdfsdfsddddddddddddddddddddddddddddddddddddddddddddddddddd" , InStock=true, Price=50, ImageUrl="../../Assets/Products/tv.jpg"},
                new Product { Id=3, Title="LG Smart TV 3", Description="lsdfsdfsssssssssssssssssssssdfsdfsdfsdfsddddddddddddddddddddddddddddddddddddddddddddddddddd" , InStock=true, Price=50, ImageUrl="../../Assets/Products/tv.jpg"},
            });
            ProductsFilter = new ProductsFilter { CurrentMinValue = 50, CurrentMaxValue = 50, MinValue = 0, MaxValue = 100 };
            InitCommandsAndEvents();
        }

        private void InitCommandsAndEvents()
        {
            NavigateToProductCommand = new DelegateCommand<Product>(NavigateToProduct);
            _eventAggregator.GetEvent<ShowFilterDialogEvent>().Subscribe(ShowFilterDialog);
        }

        private void ShowFilterDialog()
        {
            var param = new DialogParameters();
            param.Add("ProductsFilter", ProductsFilter);
            _dialogService.ShowDialog(nameof(FilterDialogView), param, (res) => { FilterProducts(res); });
        }

        private void FilterProducts(IDialogResult res)
        {
            
            if(res.Result==ButtonResult.OK)
                ProductsFilter=res.Parameters.GetValue<ProductsFilter>("ProductsFilter");
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
