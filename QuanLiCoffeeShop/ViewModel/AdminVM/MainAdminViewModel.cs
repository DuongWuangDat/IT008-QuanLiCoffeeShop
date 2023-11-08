using QuanLiCoffeeShop.View.Admin.StaffManagement;
using QuanLiCoffeeShop.View.Admin.CustomerManagement;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLiCoffeeShop.View.Admin;

namespace QuanLiCoffeeShop.ViewModel.AdminVM
{
    public class MainAdminViewModel : BaseViewModel
    {
        private Page currentPage;
        public Page CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }
        public ICommand LoadNhanVienPage { get; }
        public ICommand LoadKhachHangPage { get; set; }
        public MainAdminViewModel()
        {
            LoadNhanVienPage = new RelayCommand<object>(canExecute: CanExecuteLoadStaffPage, execute: ExecuteLoadStaffPage);
            LoadKhachHangPage = new RelayCommand<Frame>((p) => { return true; }, (p) => { p.Content = new CustomerPage() ; });
        }

        private void ExecuteLoadStaffPage(object obj)
        {
            CurrentPage = new StaffPage();
        }

        private bool CanExecuteLoadStaffPage(object obj)
        {
            return true;
        }
    }
}
