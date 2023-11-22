﻿using QuanLiCoffeeShop.DTOs;
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
using System.ComponentModel;

namespace QuanLiCoffeeShop.ViewModel.StaffVM.SalesVM
{
    public partial class SalesMainPageViewModel:BaseViewModel
    {
        public static List<BillInfoDTO> billInfoList;
        private ObservableCollection<BillInfoDTO> _billInfoList;

        public ObservableCollection<BillInfoDTO> BillInfoList
        {
            get { return _billInfoList; }
            set { _billInfoList = value; OnPropertyChanged(); }
        }

        private SeatDTO _selectedSeatItem;
        public SeatDTO SelectedSeatItem
        {
            get { return _selectedSeatItem; }
            set { _selectedSeatItem = value; OnPropertyChanged(); }
        }
        
        private ProductDTO _selectedPrdItem;
        public ProductDTO SelectedPrdItem
        {
            get { return _selectedPrdItem; }
            set { _selectedPrdItem = value; OnPropertyChanged(); }
        }
        private BillInfoDTO _selectedBillInfo;
        public BillInfoDTO SelectedBillInfo
        {
            get { return _selectedBillInfo; }
            set { _selectedBillInfo = value; OnPropertyChanged(); }
        }
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
        private decimal _totalBillValue;

        public decimal TotalBillValue
        {
            get { return _totalBillValue; }
            set { _totalBillValue = value; OnPropertyChanged(); }
        }

        public ICommand LoadSeatPageCM {  get; set; }
        public ICommand LoadProductPageCM { get; set; }
        public ICommand FirstLoadCM { get; set; }
        public ICommand SearchCusCM { get; set; }
        public ICommand AddCustomerCM {  get; set; }
        public ICommand DeleteBillInfoCM { get; set; }
        public ICommand SubBillInfoCM { get; set; }
        public ICommand PlusBillInfoCM { get; set; }
        public ICommand ChangeCountCM { get; set; }
        public SalesMainPageViewModel() {
            FirstLoadCM = new RelayCommand<Frame>((p) => { return true; }, (p) => {
                p.Content = new SeatPage();                
                
                BillInfoList = new ObservableCollection<BillInfoDTO>();
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
                p.Content = new ProductPage();
            });

            #region Seat

            #endregion
                        
            #region Product
            AllPrDFilter = new RelayCommand<RadioButton>((p) => { return true; },  (p) =>
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
            SelectPrd = new RelayCommand<object>((p) => { return true; }, (p) => {
                if (SelectedPrdItem != null)
                {
                    Product a = new Product { 
                        ID = SelectedPrdItem.ID,    
                        Price = SelectedPrdItem.Price,                       
                        DisplayName = SelectedPrdItem.DisplayName 
                    };
                            
                    BillInfoDTO billInfo = new BillInfoDTO
                    {

                        IDProduct = SelectedPrdItem.ID,
                        IsDeleted = SelectedPrdItem.IsDeleted,
                        PriceItem = SelectedPrdItem.Price,
                        Count = 1,
                        Product = a
                    };
                    
                    BillInfoList.Add(billInfo);
                    TotalBillValue = TotalBillValue + billInfo.PriceItem??0;
                }
                else
                {
                    MessageBox.Show("Selected Item null");
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
                        //MessageBoxCustom.Show(MessageBoxCustom.Error, messageSearch);
                    }
                }
            });
            DeleteBillInfoCM = new RelayCommand<BillInfoDTO>((p) => { return true; }, (p) => {
                SelectedBillInfo = p;
                DeleteMessage wd = new DeleteMessage();
                wd.ShowDialog();
                if (wd.DialogResult == true)
                {
                    TotalBillValue = TotalBillValue - SelectedBillInfo.PriceItem??0;
                    BillInfoList.Remove(SelectedBillInfo);
                }
            });
            SubBillInfoCM = new RelayCommand<BillInfoDTO>((p) => { return true; }, (p) => {
                SelectedBillInfo = p;
                if(SelectedBillInfo.Count>0) 
                    SelectedBillInfo.Count--;                
            });
            PlusBillInfoCM = new RelayCommand<BillInfoDTO>((p) => { return true; }, (p) => {
                SelectedBillInfo = p;
                SelectedBillInfo.Count++;                
            });
            ChangeCountCM = new RelayCommand<BillInfoDTO>((p) => { return true; }, (p) =>
            {
                SelectedBillInfo = p;
                TotalBillValue = TotalBillValue - SelectedBillInfo.PriceItem ?? 0;
                SelectedBillInfo.PriceItem = SelectedBillInfo.Count * SelectedBillInfo.Product.Price;
                TotalBillValue = TotalBillValue + SelectedBillInfo.PriceItem ?? 0;
            });
            #endregion
        }        
    }
}
