using QuanLiCoffeeShop.DTOs;
using QuanLiCoffeeShop.Model.Service;
using QuanLiCoffeeShop.View.Admin.CustomerManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLiCoffeeShop.ViewModel.AdminVM.CustomerManagementVM
{
    public class CustomerManagementViewModel : BaseViewModel
    {
        private ObservableCollection<CustomerDTO> _customerList;

        public ObservableCollection<CustomerDTO> CustomerList
        {
            get { return _customerList; }
            set { _customerList = value; OnPropertyChanged(); }
        }

        public ICommand FirstLoadCM { get; set; }
        public ICommand SearchCustomerCM { get; set; }
        public ICommand AddCusWdCM { get; set; }
        public ICommand AddCusListCM { get; set; }
        public ICommand CloseWdCM { get; set; }
        public CustomerManagementViewModel() {
            FirstLoadCM = new RelayCommand<Page>((p) => { return true; }, async (p) => 
            {
                CustomerList = new ObservableCollection<CustomerDTO>(await CustomerService.Ins.GetAllCus());
            });
            SearchCustomerCM = new RelayCommand<TextBox>((p) => { return true; }, async (p) =>
            {
                if(p.Text == "")
                {
                    CustomerList = new ObservableCollection<CustomerDTO>(await CustomerService.Ins.GetAllCus());
                }
                else
                {
                    CustomerList = new ObservableCollection<CustomerDTO>(await CustomerService.Ins.SearchCus(p.Text));
                }
               
            });
            AddCusWdCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddCustomerWindow wd = new AddCustomerWindow();
                wd.ShowDialog();
            });
            AddCusListCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

            });
            CloseWdCM = new RelayCommand<Window>((p) => { return true; }, (p) => 
            {
                p.Close();
            });
        }
    }
}
