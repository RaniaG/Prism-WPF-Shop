using E_Shop.Core.Entities;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Products.ViewModels
{
    public class CartViewModel:BindableBase
    {
        private ObservableCollection<CartItem> _products;
        public ObservableCollection<CartItem> Products
        {
            get { return _products; }
            set { SetProperty(ref _products, value); }
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
                },new CartItem{
                    Product=new Product { Id=1, Title="LG Smart TV", Description="lsdfsdfsssssssssssssssssssssdfsdfsdfsdfsddddddddddddddddddddddddddddddddddddddddddddddddddd" , InStock=true, Price=50, ImageUrl="../../Assets/Products/tv.jpg"}
                    , Count=1
                },new CartItem{
                    Product=new Product { Id=1, Title="LG Smart TV", Description="lsdfsdfsssssssssssssssssssssdfsdfsdfsdfsddddddddddddddddddddddddddddddddddddddddddddddddddd" , InStock=true, Price=50, ImageUrl="../../Assets/Products/tv.jpg"}
                    , Count=1
                },new CartItem{
                    Product=new Product { Id=1, Title="LG Smart TV", Description="lsdfsdfsssssssssssssssssssssdfsdfsdfsdfsddddddddddddddddddddddddddddddddddddddddddddddddddd" , InStock=true, Price=50, ImageUrl="../../Assets/Products/tv.jpg"}
                    , Count=1
                },new CartItem{
                    Product=new Product { Id=1, Title="LG Smart TV", Description="lsdfsdfsssssssssssssssssssssdfsdfsdfsdfsddddddddddddddddddddddddddddddddddddddddddddddddddd" , InStock=true, Price=50, ImageUrl="../../Assets/Products/tv.jpg"}
                    , Count=1
                },new CartItem{
                    Product=new Product { Id=1, Title="LG Smart TV", Description="lsdfsdfsssssssssssssssssssssdfsdfsdfsdfsddddddddddddddddddddddddddddddddddddddddddddddddddd" , InStock=true, Price=50, ImageUrl="../../Assets/Products/tv.jpg"}
                    , Count=1
                },new CartItem{
                    Product=new Product { Id=1, Title="LG Smart TV", Description="lsdfsdfsssssssssssssssssssssdfsdfsdfsdfsddddddddddddddddddddddddddddddddddddddddddddddddddd" , InStock=true, Price=50, ImageUrl="../../Assets/Products/tv.jpg"}
                    , Count=1
                },new CartItem{
                    Product=new Product { Id=1, Title="LG Smart TV", Description="lsdfsdfsssssssssssssssssssssdfsdfsdfsdfsddddddddddddddddddddddddddddddddddddddddddddddddddd" , InStock=true, Price=50, ImageUrl="../../Assets/Products/tv.jpg"}
                    , Count=1
                },
                new CartItem{
                    Product=new Product { Id=1, Title="LG Smart TV 2", Description="lsdfsdfsssssssssssssssssssssdfsdfsdfsdfsddddddddddddddddddddddddddddddddddddddddddddddddddd" , InStock=false, Price=50, ImageUrl="../../Assets/Products/tv.jpg"}
                    , Count=1
                }
            });
            InitCommands();
        }

        private void InitCommands()
        {
            SubmitCommand = new DelegateCommand(Submit);
            DeleteCommand = new DelegateCommand<CartItem>(Delete);

        }

        private void Delete(CartItem obj)
        {
            throw new NotImplementedException();
        }

        private void Submit()
        {
            throw new NotImplementedException();
        }
    }
}
