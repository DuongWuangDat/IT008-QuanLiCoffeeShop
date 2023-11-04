using QuanLiCoffeeShop.View.Admin.StaffManagement;
using System.Windows.Controls;
using System.Windows.Input;

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
        public MainAdminViewModel()
        {
            LoadNhanVienPage = new RelayCommand<object>(canExecute: CanExecuteLoadStaffPage, execute: ExecuteLoadStaffPage);
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
