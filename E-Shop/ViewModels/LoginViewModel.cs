using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.ViewModels
{
    public class LoginViewModel : BindableBase, IDialogAware
    {
        public string Title => "Login";

        public event Action<IDialogResult> RequestClose;

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
        public LoginViewModel()
        {
            //inject login service

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
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {

        }
    }
}
