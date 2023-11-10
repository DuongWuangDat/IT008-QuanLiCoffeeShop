using QuanLiCoffeeShop.View.Admin;
using QuanLiCoffeeShop.View.Admin.StaffManagement;
using QuanLiCoffeeShop.View.Admin.CustomerManagement;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLiCoffeeShop.View.Admin;

namespace QuanLiCoffeeShop.ViewModel.AdminVM
{
    internal class MainAdminViewModel : BaseViewModel
    {
    
        private StaffPage staffPage { get; set; }
        private CustomerPage cusPage { get; set; }
        public ICommand LoadNhanVienPage { get; }
        public ICommand LoadKhachHangPage { get; set; }
        public MainAdminViewModel()
        {
            staffPage = new StaffPage();
            cusPage = new CustomerPage();
            LoadNhanVienPage = new RelayCommand<Frame>((p) => { return true; }, (p) => { p.Content = staffPage; });
            LoadKhachHangPage = new RelayCommand<Frame>((p) => { return true; }, (p) => { p.Content = cusPage; });
        }
    }
}
