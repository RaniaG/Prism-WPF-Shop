using E_Shop.Core.Consts;
using E_Shop.Views;
using E_Shop.Views.Products;
using Prism.Mvvm;
using Prism.Regions;

namespace E_Shop.ViewModels
{
    public class ShellWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private readonly IRegionManager _regionManager;
        public ShellWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            RegisterViewsWithRegions();
        }
        private void RegisterViewsWithRegions()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.HeaderRegion, typeof(HeaderView));
            _regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(LoginView));
            _regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(HomeContainerView));
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(ProductsListView));

        }
    }
}
