using E_Shop.Consts;
using E_Shop.Core.Consts;
using E_Shop.Core.Events;
using E_Shop.Dialogs;
using E_Shop.Entities.Interfaces.Services;
using E_Shop.Models;
using E_Shop.Services;
using E_Shop.Views.Products;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.ViewModels.Products
{
    public class ProductDetailsViewModel : BindableBase, INavigationAware
    {
        private CartItemModel _productCartItem;
        public CartItemModel ProductCartItem
        {
            get { return _productCartItem; }
            set { SetProperty(ref _productCartItem, value); }
        }
        private string _selectedImageUrl="";
        public string SelectedImageUrl
        {
            get { return _selectedImageUrl; }
            set { SetProperty(ref _selectedImageUrl, value); }
        }
        private int _selecteImageIndex;
        public int SelectedImageIndex
        {
            get { return _selecteImageIndex; }
            set { SetProperty(ref _selecteImageIndex, value); }
        }
        public DelegateCommand<string> UpdateCountCommand { get; set; }
        public DelegateCommand AddToCartCommand { get; set; }
        public DelegateCommand NextImageCommand { get; set; }
        public DelegateCommand PreviousImageCommand { get; set; }




        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        private readonly IUserService _userService;
        private readonly ICartService _cartService;
        private readonly IDialogService _dialogService;
        private readonly IMessageResourceManager _messageResourceManager;




        public ProductDetailsViewModel(IEventAggregator eventAggregator, IRegionManager regionManager,
            IUserService userService,ICartService cartService,IDialogService dialogService,
            IMessageResourceManager messageResourceManager)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _userService = userService;
            _cartService = cartService;
            _dialogService = dialogService;
            _messageResourceManager = messageResourceManager;
            InitCommands();
        }
        private void InitCommands()
        {
            UpdateCountCommand = new DelegateCommand<string>(UpdateCount);
            AddToCartCommand = new DelegateCommand(AddToCart);
            NextImageCommand = new DelegateCommand(()=> { ChangeImage("Right"); },()=> { return CanChangeImage("Right"); });
            PreviousImageCommand = new DelegateCommand(() => { ChangeImage("Left");  }, () => { return CanChangeImage("Left"); });
        }

        private bool CanChangeImage(string arg)
        {
            var result = false;
            if (ProductCartItem == null)
                return result;
            switch (arg)
            {
                case "Right":
                    result= SelectedImageIndex < ProductCartItem.Product.Images.Count - 1;
                    break;
                case "Left":
                    result= SelectedImageIndex > 0;
                    break;
            }
            return result;
        }

        private void ChangeImage(string obj)
        {
            switch (obj)
            {
                case "Right":
                    SelectedImageIndex++;
                    break;
                case "Left":
                    SelectedImageIndex--;
                    break;
            }
            SelectedImageUrl =ProductCartItem.Product.Images[SelectedImageIndex];
            PreviousImageCommand.RaiseCanExecuteChanged();
            NextImageCommand.RaiseCanExecuteChanged();

        }

        private void AddToCart()
        {
            var userId = _userService.GetCurrentUser().Id;
            _cartService.AddToCart(new Entities.CartItem { Count = ProductCartItem.Count, ProductId = ProductCartItem.Product.Id, UserId = userId });
            var eventPayload = new CartItemEventModel
            {
                Product = ProductCartItem.Product,
                Count = ProductCartItem.Count,
                Action=CartAction.Add
            };
            _eventAggregator.GetEvent<UpdateCartEvent>().Publish(eventPayload);
            _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(ProductsListView));
            var dialogParams = new DialogParameters();

            dialogParams.Add("Message", _messageResourceManager.GetMessage(Messages.CartUpdateSuccess));
            dialogParams.Add("Type", MessageDialogType.Success);
            _dialogService.ShowDialog(nameof(MessageDialogView), dialogParams, (res) => { });

        }

        private void UpdateCount(string obj)
        {
            switch (obj)
            {
                case "Inc":
                    ProductCartItem.Count++;
                    break;
                case "Dec":
                    if (ProductCartItem.Count >1)
                        ProductCartItem.Count--;
                    break;
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            ProductCartItem = new CartItemModel()
            {
                Product = navigationContext.Parameters.GetValue<ProductModel>("ProductModel")
            };
            SelectedImageUrl = ProductCartItem.Product.Images.Any()? ProductCartItem.Product.Images[SelectedImageIndex]:"";
            NextImageCommand.RaiseCanExecuteChanged();
        }
    }
}
