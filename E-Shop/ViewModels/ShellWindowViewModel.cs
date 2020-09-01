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

        public ShellWindowViewModel()
        {
        }
    }
}
