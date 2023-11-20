using QuanLiCoffeeShop.DTOs;
using QuanLiCoffeeShop.Model;
using QuanLiCoffeeShop.Model.Service;
using QuanLiCoffeeShop.Utils;
using QuanLiCoffeeShop.View.Staff.Sales;
using QuanLiCoffeeShop.View.MessageBox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Microsoft.Win32;

namespace QuanLiCoffeeShop.ViewModel.StaffVM.SalesVM
{
    public partial class SalesMainPageViewModel:BaseViewModel
    {
        public ICommand LoadSeatPageCM {  get; set; }
        public ICommand LoadProductPageCM { get; set; }
        public ICommand FirstLoadCM { get; set; }
        public SalesMainPageViewModel() {
            FirstLoadCM = new RelayCommand<Frame>((p) => { return true; }, (p) => {
                p.Content = new SeatPage();
            });
            LoadSeatPageCM = new RelayCommand<Frame>((p)=> { return true; }, (p)=> {
                p.Content = new SeatPage();
            });
            LoadProductPageCM = new RelayCommand<Frame>((p) => { return true; }, async (p) => {
                ProductList = new ObservableCollection<ProductDTO>(await ProductService.Ins.GetAllProduct());
                if (ProductList != null)
                {
                    prdList = new List<ProductDTO>(ProductList);
                }
                p.Content = new View.Staff.Sales.ProductPage();
            });

            #region Product
            AllPrDFilter = new RelayCommand<RadioButton>((p) => { return true; }, async (p) =>
            {
                ProductList = new ObservableCollection<ProductDTO>(prdList);
            });
            ProductFilter = new RelayCommand<RadioButton>((p) => { return true; }, (p) =>
            {
                ProductList = new ObservableCollection<ProductDTO>(prdList.FindAll(x => x.GenreName.ToLower().Contains(p.Content.ToString().ToLower())));
            });
            SearchProductCM = new RelayCommand<TextBox>((p) => { return true; }, async (p) =>
            {
                if (p.Text == "")
                {
                    ProductList = new ObservableCollection<ProductDTO>(await ProductService.Ins.GetAllProduct());
                }
                else
                {
                    ProductList = new ObservableCollection<ProductDTO>(prdList.FindAll(x => x.DisplayName.ToLower().Contains(p.Text.ToLower())));
                }

            });
            #endregion
        }
    }
}
