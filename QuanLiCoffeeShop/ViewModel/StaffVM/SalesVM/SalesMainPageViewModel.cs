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
using QuanLiCoffeeShop.View.Admin.CustomerManagement;

namespace QuanLiCoffeeShop.ViewModel.StaffVM.SalesVM
{
    public partial class SalesMainPageViewModel:BaseViewModel
    {
        //class InfoBillProduct
        //{
        //    public string DisplayName { get; set; }
        //    public string Description { get; set; }
        //    public int Count {  get; set; }
        //    public int PriceItem {  get; set; }
        //    public int TotalPrice { get
        //        {

        //        } }
        //}
        private string _tableName;
        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; OnPropertyChanged(); }
        }
        private Customer _cusOfBill;
        public Customer CusOfBill
        {
            get { return _cusOfBill; }
            set { _cusOfBill = value; OnPropertyChanged(); }
        }
        private string _cusInfo;
        public string CusInfo
        {
            get { return _cusInfo; }
            set { _cusInfo = value; OnPropertyChanged(); }
        }
        private string _totalBillValue;

        public string TotalBillValue
        {
            get { return _totalBillValue; }
            set { _totalBillValue = value; OnPropertyChanged(); }
        }

        public ICommand LoadSeatPageCM {  get; set; }
        public ICommand LoadProductPageCM { get; set; }
        public ICommand FirstLoadCM { get; set; }
        public ICommand SearchCusCM { get; set; }
        public ICommand AddCustomerCM {  get; set; }
        public SalesMainPageViewModel() {
            FirstLoadCM = new RelayCommand<Frame>((p) => { return true; }, (p) => {
                p.Content = new SeatPage();
            });
            LoadSeatPageCM = new RelayCommand<Frame>((p)=> { return true; }, (p)=> {
                p.Content = new SeatPage();
            });
            LoadProductPageCM = new RelayCommand<Frame>((p) => { return true; }, async (p) => {
                ProductList = new ObservableCollection<ProductDTO>(await ProductService.Ins.GetAllProductCounted());
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
            #region Bill
            AddCustomerCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddCustomerWindow wd = new AddCustomerWindow();
                wd.ShowDialog();
            });
            SearchCusCM = new RelayCommand<object>((p) => { return true; }, async(p) =>
            {              
                (Customer a, bool success, string messageSearch) = await CustomerService.Ins.findCusbyPhone(CusInfo);
                if (a != null)
                {
                    CusOfBill = a;
                }
                else
                {
                    (Customer b, bool success1, string messageSearch1) = await CustomerService.Ins.findCusbyEmail(CusInfo);
                    if(b!= null)
                    {
                        CusOfBill = b;
                    }
                    else
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, messageSearch);
                    }
                }
            });
            #endregion
        }
    }
}
