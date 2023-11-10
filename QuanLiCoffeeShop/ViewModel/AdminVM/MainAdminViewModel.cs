using QuanLiCoffeeShop.View.Admin;
using QuanLiCoffeeShop.View.Admin.StaffManagement;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLiCoffeeShop.ViewModel.AdminVM
{
    internal class MainAdminViewModel : BaseViewModel
    {
        private StaffPage staffPage;

        public ICommand LoadNhanVienPage { get; }
        public MainAdminViewModel()
        {
            staffPage = new StaffPage();
            LoadNhanVienPage = new RelayCommand<Frame>(null, (p) =>
            {
                p.Content = staffPage;
            });
        }
    }
}
