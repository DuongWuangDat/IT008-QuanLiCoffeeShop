using QuanLiCoffeeShop.DTOs;
using QuanLiCoffeeShop.View.Admin;
using QuanLiCoffeeShop.View.Admin.SanPham;
using QuanLiCoffeeShop.View.Admin.StaffManagement;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLiCoffeeShop.ViewModel.AdminVM
{
    internal class MainAdminViewModel : BaseViewModel
    {
        public static StaffDTO curentstaff;
        public ICommand FirstLoadCM { get; set; }
        public ICommand LoadNhanVienPage { get; }
        public ICommand LoadKhachHangPage { get; set; }
        public ICommand LoadSanPhamPage { get; set; }
        public MainAdminViewModel()
        {
            FirstLoadCM = new RelayCommand<Frame>((p) => { return true; }, (p)=> { p.Content = new SanPhamPage(); });
            LoadNhanVienPage = new RelayCommand<Frame>((p) => { return true; }, (p) => { p.Content = new StaffPage(); });
            LoadKhachHangPage = new RelayCommand<Frame>((p) => { return true; }, (p) => { p.Content = new CustomerPage(); });
            LoadSanPhamPage = new RelayCommand<Frame>((p) => { return true; }, (p) => { p.Content = new SanPhamPage(); });
        }
    }
}
