using QuanLiCoffeeShop.DTOs;
using QuanLiCoffeeShop.Model;
using QuanLiCoffeeShop.View.Admin.StaffManagement;
using QuanLiCoffeeShop.ViewModel.AdminVM.StaffManagementVM;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace QuanLiCoffeeShop.ViewModel
{


    public class StaffViewModel : BaseViewModel
    {
        public ObservableCollection<StaffDTO> Employees { get; set; }
        public ICommand OpenAddWindowCommand { get; }

        public StaffViewModel()
        {
            Employees = new ObservableCollection<StaffDTO>();
            OpenAddWindowCommand = new RelayCommand<object>(null, execute: OpenNewWindow);
        }

        private void OpenNewWindow(object obj)
        {
            var addStaffWindow = new AddStaff();
            addStaffWindow.ShowDialog();
        }
    }
}
