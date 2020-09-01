using E_Shop.Business.Services;
using E_Shop.Core.Consts;
using E_Shop.DAL.Repositories;
using E_Shop.DAL.XMLReader;
using E_Shop.Dialogs;
using E_Shop.Entities.Interfaces.Repositories;
using E_Shop.Entities.Interfaces.Services;
using E_Shop.Services;
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

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var regionManager= Container.Resolve<IRegionManager>();

            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(LoginView));
            regionManager.RegisterViewWithRegion(RegionNames.HeaderRegion, typeof(HeaderView));
            regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(ProductsListView));
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterForNavigation(containerRegistry);
            RegisterDependencyInjectionTypes(containerRegistry);
            RegisterDialogs(containerRegistry);


        }
        private void RegisterDependencyInjectionTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IXMLReader, XMLReader>();
            containerRegistry.RegisterSingleton<IXMLWriter, XMLWriter>();

            containerRegistry.RegisterSingleton<IUserRepository, UserRepository>();
            containerRegistry.RegisterSingleton<IProductRepository, ProductRepository>();
            containerRegistry.RegisterSingleton<ICartRepository, CartRepository>();

            containerRegistry.RegisterSingleton<IUserService, UserService>();
            containerRegistry.RegisterSingleton<IProductService, ProductService>();
            containerRegistry.RegisterSingleton<ICartService, CartService>();

            containerRegistry.RegisterSingleton<IMessageResourceManager, MessageResourceManager>();

        }

        private void RegisterForNavigation(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>();
            containerRegistry.RegisterForNavigation<HomeContainerView>();
            containerRegistry.RegisterForNavigation<HeaderView, HeaderViewModel>();
            containerRegistry.RegisterForNavigation<ProductsListView, ProductsListViewModel>();
            containerRegistry.RegisterForNavigation<ProductDetailsView, ProductDetailsViewModel>();
            containerRegistry.RegisterForNavigation<CartView, CartViewModel>();
        }
        private void RegisterDialogs(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<FilterDialogView, FilterDialogViewModel>();
            containerRegistry.RegisterDialog<MessageDialogView, MessageDialogViewModel>();
        }


    }
}
