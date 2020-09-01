using E_Shop.Consts;
using E_Shop.Core.Consts;
using E_Shop.Dialogs;
using E_Shop.Entities.Interfaces.Services;
using E_Shop.Services;
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
using System.Text.RegularExpressions;
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
      
        public DelegateCommand LoginCommand { get; set; }

        private readonly IRegionManager _regionManager;
        private readonly IUserService _userService;
        private readonly IDialogService _dialogService;
        private readonly IMessageResourceManager _messageResourceManager;



        public LoginViewModel(IRegionManager regionManager,IUserService userService,IDialogService dialogService,
            IMessageResourceManager messageResourceManager)
        {
            //inject login service
            _regionManager = regionManager;
            _userService = userService;
            _dialogService = dialogService;
            _messageResourceManager = messageResourceManager;

            LoginCommand = new DelegateCommand(Login, CanLogin);
            LoginCommand.ObservesProperty(() => Username);

        }

        private bool CanLogin()
        {
            return !(string.IsNullOrEmpty(Username) || string.IsNullOrWhiteSpace(Username));
        }

        private void Login()
        {
            if (!ValidateUsername())
            {
                var error = _messageResourceManager.GetMessage(Messages.NameValidationError);
                ShowErrorDialog(error);
            }
            else
            {
                var loginSuccess = _userService.Login(Username);
                if (!loginSuccess)
                {
                    ShowErrorDialog(_messageResourceManager.GetMessage(Messages.UserNotFound));
                }
                else
                {
                    _regionManager.RequestNavigate(RegionNames.MainRegion, nameof(HomeContainerView));
                    _regionManager.RequestNavigate(RegionNames.HeaderRegion, nameof(HeaderView));
                    _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(ProductsListView));
                }
            }
            
        }
        private bool ValidateUsername()
        {
            return !string.IsNullOrEmpty(Username) && Regex.Match(Username, @"^[a-zA-Z0-9]*$").Success;
        }
        private void ShowErrorDialog(string errorMessage)
        {
            var dialogParams = new DialogParameters();
            dialogParams.Add("Message", errorMessage);
            dialogParams.Add("Type", MessageDialogType.Error);
            _dialogService.ShowDialog(nameof(MessageDialogView), dialogParams,(res)=> { });
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
