using E_Shop.Consts;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace E_Shop.Dialogs
{
    public class MessageDialogViewModel : BindableBase, IDialogAware
    {
        public string Title { get; set; }
        public MessageDialogType Type { get; set; }

        public event Action<IDialogResult> RequestClose;

        private bool _isError;
        public bool IsError
        {
            get { return _isError; }
            set { SetProperty(ref _isError, value); }
        }

        private Brush _color;
        public Brush Color
        {
            get { return _color; }
            set { SetProperty(ref _color, value); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
        public DelegateCommand CloseMessageCommand { get; set; }
        public MessageDialogViewModel()
        {
            CloseMessageCommand = new DelegateCommand(OnDialogClosed);
        }
        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            RequestClose?.Invoke(new DialogResult());
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Message = parameters.GetValue<string>("Message");
            Type = parameters.GetValue<MessageDialogType>("Type");
            switch (Type)
            {
                case MessageDialogType.Error:
                    Title = "Error";
                    Color = Brushes.Red;
                    IsError = true;
                    break;
                case MessageDialogType.Success:
                    Title = "Success";
                    Color = Brushes.Green;
                    IsError = false;
                    break;
            }
        }
    }
}
