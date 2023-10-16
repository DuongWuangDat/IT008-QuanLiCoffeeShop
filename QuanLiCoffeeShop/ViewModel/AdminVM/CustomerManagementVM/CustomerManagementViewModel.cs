using QuanLiCoffeeShop.DTOs;
using QuanLiCoffeeShop.Model;
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
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _email ;

        public string Email
        {
            get { return _email ; }
            set { _email  = value; }
        }
        private string _phoneNumber;

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }
        private string _spend;

        public string Spend
        {
            get { return _spend; }
            set { _spend = value; }
        }
        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
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
            AddCusListCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                Customer newCus = new Customer
                {
                    Description = this.Description,
                    DisplayName = this.Name,
                    PhoneNumber = this.PhoneNumber,
                    Spend = decimal.Parse(this.Spend),
                    Email = this.Email,
                    IsDeleted = false,
                    
                };
                (bool IsAdded, string messageAdd) = await CustomerService.Ins.AddNewCus(newCus);
                if(IsAdded)
                {
                    p.Close();
                    CustomerList= new ObservableCollection<CustomerDTO>(await CustomerService.Ins.GetAllCus());
                }
                else
                {
                    MessageBox.Show(messageAdd);
                }
            });
            CloseWdCM = new RelayCommand<Window>((p) => { return true; }, (p) => 
            {
                p.Close();
            });
        }
    }
}
