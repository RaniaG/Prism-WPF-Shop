using E_Shop.Core.Consts;
using E_Shop.Core.Dialogs;
using E_Shop.Products.Dialogs;
using E_Shop.Products.ViewModels;
using E_Shop.Products.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace E_Shop.Products
{
    public class ProductsModule : IModule
    {
        public ProductsModule(IRegionManager regionManager)
        {
            regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(ProductsListView));
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
 
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ProductsListView, ProductsListViewModel>();
            containerRegistry.RegisterForNavigation<ProductDetailsView, ProductDetailsViewModel>();
            containerRegistry.RegisterForNavigation<CartView, CartViewModel>();


            containerRegistry.RegisterDialog<FilterDialogView, FilterDialogViewModel>();
        }
    }
}