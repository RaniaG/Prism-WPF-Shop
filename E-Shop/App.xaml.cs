using E_Shop.Dialogs;
using E_Shop.ViewModels;
using E_Shop.ViewModels.Products;
using E_Shop.Views;
using E_Shop.Views.Products;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Services.Dialogs;
using System.Windows;

namespace E_Shop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {

        protected override Window CreateShell()
        {
            return new ShellWindow();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>();
            containerRegistry.RegisterForNavigation<HomeContainerView>();
            containerRegistry.RegisterForNavigation<ProductsListView, ProductsListViewModel>();
            containerRegistry.RegisterForNavigation<ProductDetailsView, ProductDetailsViewModel>();
            containerRegistry.RegisterForNavigation<CartView, CartViewModel>();
            containerRegistry.RegisterDialog<FilterDialogView, FilterDialogViewModel>();
        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
        }

       

    }
}
