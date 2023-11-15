using QuanLiCoffeeShop.View.Admin;
using QuanLiCoffeeShop.View.Admin.StaffManagement;
using QuanLiCoffeeShop.View.Admin.CustomerManagement;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System;
using System.Windows;
using QuanLiCoffeeShop.View.Admin.SanPham;
using QuanLiCoffeeShop.DTOs;
using QuanLiCoffeeShop.View.Admin.ThongKe;
using LiveCharts;

namespace QuanLiCoffeeShop.ViewModel.AdminVM
{
    internal class MainAdminViewModel : BaseViewModel
    {
        private SeriesCollection _revenueSeries;
        public SeriesCollection RevenueSeries
        {
            get { return _revenueSeries; }
            set { _revenueSeries = value; OnPropertyChanged(); }
        }

        public static StaffDTO curentstaff;
        private string _currentName;

        public string currentName
        {
            get { return _currentName; }
            set { _currentName = value; OnPropertyChanged(); }
        }

        public ICommand FirstLoadCM { get; set; }
        public ICommand LoadNhanVienPage { get; }
        public ICommand LoadKhachHangPage { get; set; }
        public ICommand LoadSanPhamPage { get; set; }
        public ICommand LoadThongKePage { get; set; }
        public MainAdminViewModel()
        {
            FirstLoadCM = new RelayCommand<Frame>((p) => { return true; }, (p)=> { p.Content = new SanPhamPage(); currentName = curentstaff.DisplayName; });
            LoadNhanVienPage = new RelayCommand<Frame>((p) => { return true; }, (p) => { p.Content = new StaffPage(); });
            LoadKhachHangPage = new RelayCommand<Frame>((p) => { return true; }, (p) => { p.Content = new CustomerPage(); });
            LoadSanPhamPage = new RelayCommand<Frame>((p) => { return true; }, (p) => { p.Content = new SanPhamPage(); });
            LoadThongKePage = new RelayCommand<Frame>((p) => { return true; }, (p) => { p.Content = new ThongKeMainPage(); });
        }
    }
}
