using QuanLiCoffeeShop.View.Admin.StaffManagement;
using QuanLiCoffeeShop.View.Admin.CustomerManagement;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLiCoffeeShop.View.Admin;
using System.Windows.Media.Animation;
using System;
using System.Windows;
using QuanLiCoffeeShop.View.Admin.SanPham;
using QuanLiCoffeeShop.DTOs;

namespace QuanLiCoffeeShop.ViewModel.AdminVM
{
    public class MainAdminViewModel : BaseViewModel
    {
        public static StaffDTO curentstaff;
        public ICommand FirstLoadCM { get; set; }
        public ICommand LoadNhanVienPage { get; }
        public ICommand LoadKhachHangPage { get; set; }
        public MainAdminViewModel()
        {
            FirstLoadCM = new RelayCommand<Frame>((p) => { return true; }, (p)=> { p.Content = new SanPhamPage(); });
            LoadNhanVienPage = new RelayCommand<Frame>((p) => { return true; }, (p) => { p.Content = new StaffPage(); });
            LoadKhachHangPage = new RelayCommand<Frame>((p) => { return true; }, (p) => { p.Content = new CustomerPage(); });
        }
    }
}
