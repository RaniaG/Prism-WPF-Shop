using E_Shop.Products;
using E_Shop.ViewModels;
using E_Shop.Views;
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
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            if(IsLoggedIn())
            {
                var mainWindow=Container.Resolve<MainWindow>();
                mainWindow.Loaded += (_, __) =>
                {
                    Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                    mainWindow.Activate();
                };
                return mainWindow;
            }
            else
            {
                Current.Shutdown();
                return null;
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<LoginView, LoginViewModel>();
        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<ProductsModule>();
        }

        private bool IsLoggedIn()
        {
            var result = false;
            var dialogService = Container.Resolve<IDialogService>();
            var dialogParams = new DialogParameters();
            dialogService.ShowDialog("LoginView", dialogParams, (r) => {
                if (r.Result == ButtonResult.OK)
                    result = true;
            });
            return result;
        }

    }
}
