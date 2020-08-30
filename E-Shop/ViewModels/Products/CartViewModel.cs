using E_Shop.Core.Consts;
using E_Shop.Core.Entities;
using E_Shop.Views.Products;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.ViewModels.Products
{
    public class CartViewModel : BindableBase
    {
        private ObservableCollection<CartItem> _products;
        public ObservableCollection<CartItem> Products
        {
            get { return _products; }
            set
            {
                SetProperty(ref _products, value);
            }
        }

        private bool _cartNotEmpty;
        public bool CartNotEmpty
        {
            get { return _cartNotEmpty; }
            set { SetProperty(ref _cartNotEmpty, value); }
        }

        public DelegateCommand SubmitCommand { get; set; }
        public DelegateCommand<CartItem> DeleteCommand { get; set; }
        private readonly IRegionManager _regionManager;

        public CartViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            Products = new ObservableCollection<CartItem>(new List<CartItem>
            {
                new CartItem{
                    Product=new Product { Id=1, Title="LG Smart TV", Description="lsdfsdfsssssssssssssssssssssdfsdfsdfsdfsddddddddddddddddddddddddddddddddddddddddddddddddddd" , InStock=true, Price=50, ImageUrl="../../Assets/Products/tv.jpg"}
                    , Count=1
                },
                new CartItem{
                    Product=new Product { Id=1, Title="LG Smart TV", Description="lsdfsdfsssssssssssssssssssssdfsdfsdfsdfsddddddddddddddddddddddddddddddddddddddddddddddddddd" , InStock=true, Price=50, ImageUrl="../../Assets/Products/tv.jpg"}
                    , Count=1
                },new CartItem{
                    Product=new Product { Id=1, Title="LG Smart TV", Description="lsdfsdfsssssssssssssssssssssdfsdfsdfsdfsddddddddddddddddddddddddddddddddddddddddddddddddddd" , InStock=true, Price=50, ImageUrl="../../Assets/Products/tv.jpg"}
                    , Count=1
                },new CartItem{
                    Product=new Product { Id=1, Title="LG Smart TV", Description="lsdfsdfsssssssssssssssssssssdfsdfsdfsdfsddddddddddddddddddddddddddddddddddddddddddddddddddd" , InStock=true, Price=50, ImageUrl="../../Assets/Products/tv.jpg"}
                    , Count=1
                },
                //new CartItem{
                //    Product=new Product { Id=1, Title="LG Smart TV", Description="lsdfsdfsssssssssssssssssssssdfsdfsdfsdfsddddddddddddddddddddddddddddddddddddddddddddddddddd" , InStock=true, Price=50, ImageUrl="../../Assets/Products/tv.jpg"}
                //    , Count=1
                //},new CartItem{
                //    Product=new Product { Id=1, Title="LG Smart TV", Description="lsdfsdfsssssssssssssssssssssdfsdfsdfsdfsddddddddddddddddddddddddddddddddddddddddddddddddddd" , InStock=true, Price=50, ImageUrl="../../Assets/Products/tv.jpg"}
                //    , Count=1
                //},new CartItem{
                //    Product=new Product { Id=1, Title="LG Smart TV", Description="lsdfsdfsssssssssssssssssssssdfsdfsdfsdfsddddddddddddddddddddddddddddddddddddddddddddddddddd" , InStock=true, Price=50, ImageUrl="../../Assets/Products/tv.jpg"}
                //    , Count=1
                //},new CartItem{
                //    Product=new Product { Id=1, Title="LG Smart TV", Description="lsdfsdfsssssssssssssssssssssdfsdfsdfsdfsddddddddddddddddddddddddddddddddddddddddddddddddddd" , InStock=true, Price=50, ImageUrl="../../Assets/Products/tv.jpg"}
                //    , Count=1
                //},new CartItem{
                //    Product=new Product { Id=1, Title="LG Smart TV", Description="lsdfsdfsssssssssssssssssssssdfsdfsdfsdfsddddddddddddddddddddddddddddddddddddddddddddddddddd" , InStock=true, Price=50, ImageUrl="../../Assets/Products/tv.jpg"}
                //    , Count=1
                //},new CartItem{
                //    Product=new Product { Id=1, Title="LG Smart TV", Description="lsdfsdfsssssssssssssssssssssdfsdfsdfsdfsddddddddddddddddddddddddddddddddddddddddddddddddddd" , InStock=true, Price=50, ImageUrl="../../Assets/Products/tv.jpg"}
                //    , Count=1
                //},new CartItem{
                //    Product=new Product { Id=1, Title="LG Smart TV", Description="lsdfsdfsssssssssssssssssssssdfsdfsdfsdfsddddddddddddddddddddddddddddddddddddddddddddddddddd" , InStock=true, Price=50, ImageUrl="../../Assets/Products/tv.jpg"}
                //    , Count=1
                //},
                //new CartItem{
                //    Product=new Product { Id=1, Title="LG Smart TV 2", Description="lsdfsdfsssssssssssssssssssssdfsdfsdfsdfsddddddddddddddddddddddddddddddddddddddddddddddddddd" , InStock=false, Price=50, ImageUrl="../../Assets/Products/tv.jpg"}
                //    , Count=1
                //}
            });
            CartNotEmpty = true;
            InitCommands();
        }

        private void InitCommands()
        {
            SubmitCommand = new DelegateCommand(Submit, CanSubmit);
            SubmitCommand.ObservesCanExecute(() => CartNotEmpty);
            DeleteCommand = new DelegateCommand<CartItem>(Delete);

        }

        private bool CanSubmit()
        {
            return Products.Count > 0;
        }

        private void Delete(CartItem obj)
        {
            Products.Remove(obj);
            if (Products.Count == 0)
                CartNotEmpty = false;
            else CartNotEmpty = true;
        }

        private void Submit()
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(ProductsListView));
        }
    }
}
