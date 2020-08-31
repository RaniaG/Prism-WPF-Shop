using E_Shop.Core.Consts;
using E_Shop.Entities.Interfaces.Services;
using E_Shop.Views;
using E_Shop.Views.Products;
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
            set { SetProperty(ref _username, value); ErrorMessage = ""; }
        }
        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }
        public DelegateCommand LoginCommand { get; set; }

        private readonly IRegionManager _regionManager;
        private readonly IUserService _userService;

        public LoginViewModel(IRegionManager regionManager,IUserService userService)
        {
            //inject login service
            _regionManager = regionManager;
            _userService = userService;

            LoginCommand = new DelegateCommand(Login, CanLogin);
            LoginCommand.ObservesProperty(() => Username);

        }

        private bool CanLogin()
        {
            return !(string.IsNullOrEmpty(Username) || string.IsNullOrWhiteSpace(Username));
        }

        private void Login()
        {
            var loginSuccess=_userService.Login(Username);
            if (!loginSuccess)
                ErrorMessage = "Invalid User";
            else
            {
                _regionManager.RequestNavigate(RegionNames.MainRegion, nameof(HomeContainerView));
                _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(ProductsListView));
            }
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
