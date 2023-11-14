using QuanLiCoffeeShop.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLiCoffeeShop.Model;
using QuanLiCoffeeShop.Model.Service;
using QuanLiCoffeeShop.View.Admin.CustomerManagement;
using QuanLiCoffeeShop.View.MessageBox;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;

namespace QuanLiCoffeeShop.ViewModel.AdminVM.ThongKeVM
{
    public partial class ThongKeViewModel : BaseViewModel
    {
        public class ProductBillDTO
        {
            public int ID { get; set; }
            public string DisplayName { get; set; }
            public Nullable<int> SoLuong { get; set; }
            public Nullable<int> Price {  get; set; }
            public Nullable<int> ThanhTien {  get; set; }
            public ProductBillDTO(int id, string displayName, Nullable<int> soLuong, Nullable<int> price, Nullable<int> thanhTien)
            {
                ID = id;
                DisplayName = displayName;
                SoLuong = soLuong;
                Price = price;
                ThanhTien = thanhTien;
            }
        }
        public ObservableCollection<ProductBillDTO> BillToProductBill(BillDTO bill)
        {
            ObservableCollection<ProductBillDTO> pbList = new ObservableCollection<ProductBillDTO>();
            for(int i = 0; i < bill.BillInfo.Count(); i++)
            {
                BillInfoDTO bi = bill.BillInfo[i];
                Nullable<int> sl = bi.Count, pr = int.Parse(bi.PriceItem.ToString()) ;

                ProductBillDTO pb = new ProductBillDTO
                    (
                       bi.IDProduct,bi.Product.DisplayName,sl,pr, sl*pr
                    ); 
                pbList.Add(pb);
            }
            return pbList;
        }

        private static List<BillDTO> billList;
        private ObservableCollection<BillDTO> _billList;
        public ObservableCollection<BillDTO> BillList
        {
            get { return _billList; }
            set { _billList = value;OnPropertyChanged(); }
        }
        private ObservableCollection<ProductBillDTO> _productList;

        public ObservableCollection<ProductBillDTO> ProductList
        {
            get { return _productList; }
            set { _productList = value; OnPropertyChanged(); }
        }
        private BillDTO _selectedItem;
        public BillDTO SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged();}
        }
        private string _cusName;
        public string CusName
        {
            get { return _cusName; }
            set { _cusName = value; OnPropertyChanged(); }
        }
        private string _staffName;
        public string StaffName
        {
            get { return _staffName; }
            set { _staffName = value; OnPropertyChanged(); }
        }
        private string _billDate;
        public string BillDate
        {
            get { return _billDate; }
            set { _billDate = value; OnPropertyChanged(); }
        }
        private string _billValue;
        public string BillValue
        {
            get { return _billValue; }
            set { _billValue = value; OnPropertyChanged(); }
        }
        private string _soLuong;
        public string SoLuong
        {
            get { return _soLuong; }
            set { _soLuong = value; OnPropertyChanged(); }
        }
    }
}
