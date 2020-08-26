using E_Shop.Core.Consts;
using E_Shop.Views;
using Prism.Mvvm;
using Prism.Regions;

namespace E_Shop.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        private readonly IRegionManager _regionManager;
        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            RegisterViewsWithRegions();
        }
        private void RegisterViewsWithRegions()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.HeaderRegion, typeof(HeaderView));
        }
    }
}
