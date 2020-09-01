using E_Shop.Core.Consts;
using E_Shop.Core.Events;
using E_Shop.Dialogs;
using E_Shop.Entities;
using E_Shop.Entities.Interfaces.Services;
using E_Shop.Events;
using E_Shop.Models;
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
        private ObservableCollection<ProductModel> _products;
        public ObservableCollection<ProductModel> Products
        {
            get { return _products; }
            set { SetProperty(ref _products, value); }
        }
        private ProductsFilterModel _productsFilter;
        public ProductsFilterModel ProductsFilterModel
        {
            get { return _productsFilter; }
            set { SetProperty(ref _productsFilter, value); }
        }

        public DelegateCommand<ProductModel> NavigateToProductCommand { get; set; }

        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;
        private readonly IProductService _productService;


        public ProductsListViewModel(IEventAggregator eventAggregator, IRegionManager regionManager,IDialogService dialogService,
            IProductService productService)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _dialogService = dialogService;
            _productService = productService;

            InitCommandsAndEvents();
        }
        private void InitData()
        {
            var products = _productService.GetAll(e => true);
            Products = new ObservableCollection<ProductModel>(products.Select(e=> MapProduct(e)));
            var maxPrice = (int)products.Max(e => e.Price);
            ProductsFilterModel = new ProductsFilterModel { CurrentMinValue = 0, CurrentMaxValue = maxPrice, MinValue = 0, MaxValue = maxPrice };
        }
        private void InitCommandsAndEvents()
        {
            NavigateToProductCommand = new DelegateCommand<ProductModel>(NavigateToProduct);
            var showFilterEvent = _eventAggregator.GetEvent<ShowFilterDialogEvent>();
            showFilterEvent.Subscribe(()=> {
                ShowFilterDialog();
            });
        }

        private void ShowFilterDialog()
        {
            var param = new DialogParameters();
            param.Add("ProductsFilterModel", ProductsFilterModel.Clone());
            _dialogService.ShowDialog(nameof(FilterDialogView), param, (res) => { FilterProducts(res); });
        }

        private void FilterProducts(IDialogResult res)
        {
            if(res.Result==ButtonResult.OK)
            {
                ProductsFilterModel=res.Parameters.GetValue<ProductsFilterModel>("ProductsFilterModel");
                var products = _productService.GetAll(e => e.Price>= ProductsFilterModel.CurrentMinValue&&e.Price<= ProductsFilterModel.CurrentMaxValue);
                Products = new ObservableCollection<ProductModel>(products.Select(e => MapProduct(e)));
            }
        }

        private void NavigateToProduct(ProductModel ProductModel)
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("ProductModel", ProductModel);
            _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(ProductDetailsView), navigationParams);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _eventAggregator.GetEvent<ShowFilterEvent>().Publish(true);
            InitData();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            _eventAggregator.GetEvent<ShowFilterEvent>().Publish(false);

        }
        private ProductModel MapProduct(Product product)
        {
            return new ProductModel
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                InStock = product.InStock,
                ImageUrl = product.ImageUrl,
                Images=product.Images
            };
        }
    }
}
