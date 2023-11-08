using QuanLiCoffeeShop.View.Admin.StaffManagement;
using QuanLiCoffeeShop.View.Admin.CustomerManagement;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLiCoffeeShop.View.Admin;

namespace QuanLiCoffeeShop.ViewModel.AdminVM
{
    public class MainAdminViewModel : BaseViewModel
    {
        public ICommand LoadNhanVienPage { get; }
        public ICommand LoadKhachHangPage { get; set; }
        public MainAdminViewModel()
        {
            LoadNhanVienPage = new RelayCommand<Frame>((p) => { return true; }, (p) => { p.Content = new StaffPage(); });
            LoadKhachHangPage = new RelayCommand<Frame>((p) => { return true; }, (p) => { p.Content = new CustomerPage(); });
        }
    }
}
