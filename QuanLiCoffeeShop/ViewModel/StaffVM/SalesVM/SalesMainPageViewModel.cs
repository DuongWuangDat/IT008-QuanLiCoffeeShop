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
using System.ComponentModel;

namespace QuanLiCoffeeShop.ViewModel.StaffVM.SalesVM
{
    public partial class SalesMainPageViewModel:BaseViewModel
    {
        public static StaffDTO currentStaff;
        public static List<BillInfoDTO> billInfoList;
        public static List<BillDTO> billList;
        private bool _prdEnable;

        public bool prdEnable
        {
            get { return _prdEnable; }
            set { _prdEnable = value; OnPropertyChanged(); }
        }

        private ObservableCollection<BillDTO> _billlist;
        public ObservableCollection<BillDTO> BillList
        {
            get { return _billlist; }
            set { _billlist = value; OnPropertyChanged(); }
        }
        private ObservableCollection<BillInfoDTO> _billInfoList;

        public ObservableCollection<BillInfoDTO> BillInfoList
        {
            get { return _billInfoList; }
            set { _billInfoList = value; OnPropertyChanged(); }
        }
        private BillDTO _selectedbill;
        public BillDTO SelectedBill
        {
            get { return _selectedbill; }
            set { _selectedbill = value; OnPropertyChanged(); }
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
        private SolidColorBrush _brush;
        public SolidColorBrush Brush
        {
            get { return _brush; } 
            set { _brush = value; OnPropertyChanged(); } 
        }
        private string _payContent;
        public string PayContent
        {
            get { return _payContent; }
            set { _payContent = value; OnPropertyChanged(); }
        }
        private bool _payEnabled;
        public bool PayEnabled
        {
            get { return _payEnabled; }
            set { _payEnabled = value; OnPropertyChanged(); } 
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
        public ICommand PayBill {  get; set; }
        public ICommand EndBill { get; set; }
        public SalesMainPageViewModel() {
            FirstLoadCM = new RelayCommand<Frame>((p) => { return true; }, async (p) => {
                LoadPage();
                Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFD8B4"));
                currentStaff = MainStaffViewModel.currentStaff;
                PayEnabled = false;
                PayContent = "";
                p.Content = new SeatPage();
                
                BillList = new ObservableCollection<BillDTO>();
                billList = new List<BillDTO>(BillList);
                prdEnable = false;
            });
          LoadSeatPageCM = new RelayCommand<Frame>((p)=> { return true; },async (p)=> {
                 LoadPage();
              
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
            Classify = new RelayCommand<RadioButton>((p) => { return true; }, async (p) =>
            {
                TableList = new ObservableCollection<SeatDTO>(await SeatService.Ins.GetAllSeat());
                tablelist = new List<SeatDTO>(TableList);

                if (Genre == "Tất cả loại bàn")
                {
                    switch (p.Content.ToString())
                    {
                        case "Tất cả":
                            Contentbtn = "Tất cả";
                            TableList = new ObservableCollection<SeatDTO>(await SeatService.Ins.GetAllSeat());
                            break;
                        case "Đã đặt":
                            Contentbtn = "Đã đặt";
                            TableList = new ObservableCollection<SeatDTO>(tablelist.FindAll(x => (x.Status == "Đã đặt")));
                            break;
                        case "Còn trống":
                            Contentbtn = "Còn trống";
                            TableList = new ObservableCollection<SeatDTO>(tablelist.FindAll(x => (x.Status == "Còn trống")));
                            break;
                        case "Đang sửa chữa":
                            Contentbtn = "Đang sửa chữa";
                            TableList = new ObservableCollection<SeatDTO>(tablelist.FindAll(x => (x.Status == "Đang sửa chữa")));
                            break;

                    }
                }
                else
                {
                    switch (p.Content.ToString())
                    {
                        case "Tất cả":
                            Contentbtn = "Tất cả";
                            TableList = new ObservableCollection<SeatDTO>((tablelist.FindAll(x => x.GenreName == Genre)));
                            break;
                        case "Đã đặt":
                            Contentbtn = "Đã đặt";
                            TableList = new ObservableCollection<SeatDTO>(tablelist.FindAll(x => (x.Status == "Đã đặt" && x.GenreName == Genre)));
                            break;
                        case "Còn trống":
                            Contentbtn = "Còn trống";
                            TableList = new ObservableCollection<SeatDTO>(tablelist.FindAll(x => (x.Status == "Còn trống" && x.GenreName == Genre)));
                            break;
                        case "Đang sửa chữa":
                            Contentbtn = "Đang sửa chữa";
                            TableList = new ObservableCollection<SeatDTO>(tablelist.FindAll(x => (x.Status == "Đang sửa chữa" && x.GenreName == Genre)));
                            break;

                    }
                }

            });
          
               
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
                        DisplayName = SelectedPrdItem.DisplayName, 
                        Price = SelectedPrdItem.Price,
                        IDGenre = SelectedPrdItem.IDGenre,                        
                        Count = SelectedPrdItem.Count,
                        Description = SelectedPrdItem.Description,
                        Image = SelectedPrdItem.Image,
                        IsDeleted = SelectedPrdItem.IsDeleted,
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
                    Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F0BD70"));
                    PayContent = "Thanh toán";
                    PayEnabled = true;
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
                        MessageBoxCustom.Show(MessageBoxCustom.Error, messageSearch);
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
                    if (BillInfoList.Count() > 0)
                    {
                        Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F0BD70"));
                        PayEnabled = true;
                        PayContent = "Thanh toán";
                    }
                    else
                    {
                        Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFD8B4"));
                        PayEnabled = false;
                        PayContent = "";
                    }

                }
            });
            SubBillInfoCM = new RelayCommand<BillInfoDTO>((p) => { return true; }, (p) => {
                SelectedBillInfo = p;
                if(SelectedBillInfo.Count>0) 
                    SelectedBillInfo.Count--;                
            });
            PlusBillInfoCM = new RelayCommand<BillInfoDTO>((p) => { return true; }, (p) => {
                SelectedBillInfo = p;
                if(SelectedBillInfo.Count<SelectedBillInfo.Product.Count)
                    SelectedBillInfo.Count++;                
            });
            ChangeCountCM = new RelayCommand<BillInfoDTO>((p) => { return true; }, (p) =>
            {
                SelectedBillInfo = p;
                TotalBillValue = TotalBillValue - SelectedBillInfo.PriceItem ?? 0;
                SelectedBillInfo.PriceItem = SelectedBillInfo.Count * SelectedBillInfo.Product.Price;
                TotalBillValue = TotalBillValue + SelectedBillInfo.PriceItem ?? 0;
            });
            LoadBill = new RelayCommand<SeatDTO>((p) => { return true; }, async (p) =>
            {
                SelectedSeatItem = p;
                TableName = "Bàn " + SelectedSeatItem.ID;
                SelectedBill = new BillDTO();
                if(SelectedSeatItem.Status == "Đã đặt" || (SelectedSeatItem.Status == "Đang sửa chữa"))
                {
                    SelectedBill = await BillService.Ins.getBillByIdSeat(SelectedSeatItem.ID);
                    prdEnable = false;
                }
                else
                {
                    SelectedBill = null;
                    prdEnable = true;
                }
                if (SelectedBill != null)
                {
                    
                    BillInfoList = new ObservableCollection<BillInfoDTO>(SelectedBill.BillInfo);
                    billInfoList = new List<BillInfoDTO>(BillInfoList);
                    
                    Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFD8B4"));
                    PayEnabled = false;
                    PayContent = "Đã thanh toán";

                    TotalBillValue = SelectedBill.TotalPrice??0;
                }
                else
                {
                    SelectedBill = new BillDTO();
                    BillInfoList = new ObservableCollection<BillInfoDTO>();
                    billInfoList = new List<BillInfoDTO>(BillInfoList);

                    Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFD8B4"));
                    PayEnabled = false;
                    PayContent = "";

                    TotalBillValue = 0;
                }
            });
            PayBill = new RelayCommand<object>((p) => { return true; }, async (p) => 
            {
                if(CusOfBill==null) MessageBoxCustom.Show(MessageBoxCustom.Error, "Vui lòng thêm khách hàng");
                else
                {

                    DeleteMessage wd = new DeleteMessage("Xác nhận thanh toán hóa đơn?");
                    wd.ShowDialog();
                    if (wd.DialogResult == true)
                    {
                        if (SelectedSeatItem.Status == "Đang sửa chữa")
                        {
                            MessageBoxCustom.Show(MessageBoxCustom.Error, "Bàn này đang được sửa chữa");
                        }
                        else
                        {
                            (Staff a, bool success1) = await StaffService.Ins.FindStaff(currentStaff.ID);
                            billInfoList = new List<BillInfoDTO>(BillInfoList);
                            SelectedBill.BillInfo = billInfoList;
                            SelectedBill.IDCus = CusOfBill.ID;
                            SelectedBill.IDStaff = currentStaff.ID;
                            SelectedBill.IsDeleted = false;
                            SelectedBill.CreateAt = DateTime.Now;
                            SelectedBill.Customer = CusOfBill;
                            SelectedBill.Staff = a;
                            SelectedBill.TotalPrice = TotalBillValue;
                        
                            (Seat b, bool sucess2) = await SeatService.Ins.FindSeat(SelectedSeatItem.ID);
                            SelectedBill.Seat = b;
                            SelectedBill.IDSeat = SelectedSeatItem.ID;

                            (bool isAdded, string messageAdd) = await BillService.Ins.AddNewBill(SelectedBill);
                            if(isAdded)
                            {
                                billList.Add(SelectedBill);

                                Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFD8B4"));
                                PayEnabled = false;
                                PayContent = "Đã thanh toán";

                                Seat newseat = new Seat
                                {
                                    ID = SelectedSeatItem.ID,
                                    Status = "Đã đặt",
                                    IDGenre = SelectedSeatItem.IDGenre,
                                    IsDeleted = false,
                                };
                                (bool success, string messageEdit) = await SeatService.Ins.EditSeat(newseat);
                                UpdateBtn();
                            
                                for (int i = 0;i< BillInfoList.Count; i++)
                                {
                                    Product k = BillInfoList[i].Product;
                                    Product prd = new Product
                                    {
                                        ID = k.ID,
                                        DisplayName = k.DisplayName,
                                        Price = k.Price,
                                        IDGenre = k.IDGenre,
                                        Count = k.Count - BillInfoList[i].Count,
                                        Description = k.Description,
                                        Image = k.Image,
                                        IsDeleted = k.IsDeleted,
                                    };
                                    (bool ss, string me) = await ProductService.Ins.EditPrD(prd, BillInfoList[i].Product.ID);
                                    if(ss==false) { MessageBoxCustom.Show(MessageBoxCustom.Error, "Chỉnh sửa lượng hàng thất bại!"); }
                                }
                                MessageBoxCustom.Show(MessageBoxCustom.Success, "Thành công");
                                resetData();
                            
                            }
                                                
                        }
                    }

                }
            });
            EndBill = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                DeleteMessage wd = new DeleteMessage("Xác nhận kết thúc hóa đơn?");
                wd.ShowDialog();
                if (wd.DialogResult == true)
                {
                    if (SelectedSeatItem.Status == "Đang sửa chữa")
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Bàn này đang được sửa chữa");
                    }
                    else
                    {
                        billList.Remove(SelectedBill);
                        SelectedBill = null;
                        SelectedBill = new BillDTO();
                        CusOfBill = null;
                        CusInfo = null;
                        CusOfBill = new Customer();
                        BillInfoList = null;
                        BillInfoList = new ObservableCollection<BillInfoDTO>();                  
                        Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFD8B4"));
                        PayEnabled = false;
                        PayContent = "";
                        TotalBillValue = 0;

                        Seat newseat = new Seat
                        {
                            ID = SelectedSeatItem.ID,
                            Status = "Còn trống",
                            IDGenre = SelectedSeatItem.IDGenre,
                            IsDeleted = false,
                        };
                        (bool success, string messageEdit) = await SeatService.Ins.EditSeat(newseat);
                        UpdateBtn();
                    }
                    SelectedBill = null;
                    SelectedSeatItem = null;
                        SelectedSeatItem = new SeatDTO();

                }
            });
            #endregion
            #region methods
            void resetData()
            {
                TableName = null;
                CusInfo = null;
                CusOfBill = null;
                BillInfoList = null;
                TotalBillValue = 0;
            }
            #endregion
        }
    }
}
