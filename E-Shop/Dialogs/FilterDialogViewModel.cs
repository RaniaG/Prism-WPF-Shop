using E_Shop.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Dialogs
{
    public class FilterDialogViewModel : BindableBase, IDialogAware
    {
        public string Title => "Filter Products";
        private ProductsFilterModel _productFilter;
        public ProductsFilterModel ProductFilter
        {
            get { return _productFilter; }
            set { SetProperty(ref _productFilter, value); }
        }
        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }
        public DelegateCommand ApplyFilterCommand { get; set; }
        public event Action<IDialogResult> RequestClose;
        public FilterDialogViewModel()
        {
            ApplyFilterCommand = new DelegateCommand(ApplyFilter);
        }

        private void ApplyFilter()
        {
            if (ProductFilter.CurrentMaxValue < ProductFilter.CurrentMinValue)
            {
                ErrorMessage = "Max Value cannot be less than Min Value";
                return;
            }
            else
                ErrorMessage = "";
            var dialogParams = new DialogParameters();
            dialogParams.Add("ProductsFilterModel", ProductFilter);

            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, dialogParams));
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            //RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            ProductFilter = parameters.GetValue<ProductsFilterModel>("ProductsFilterModel");
        }
    }
}
