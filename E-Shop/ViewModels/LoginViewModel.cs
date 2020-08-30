using E_Shop.Core.Consts;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.ViewModels
{
    public class LoginViewModel : BindableBase,INavigationAware
    {
        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }
        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }
        public DelegateCommand LoginCommand { get; set; }

        private readonly IRegionManager _regionManager;
        public LoginViewModel(IRegionManager regionManager)
        {
            //inject login service
            _regionManager = regionManager;

            LoginCommand = new DelegateCommand(Login, CanLogin);
            LoginCommand.ObservesProperty(() => Username);

        }

        private bool CanLogin()
        {
            return !(string.IsNullOrEmpty(Username) || string.IsNullOrWhiteSpace(Username));
        }

        private void Login()
        {
            //validate username
            //should return 
            _regionManager.RequestNavigate(RegionNames.MainRegion, "HomeContainerView");
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
