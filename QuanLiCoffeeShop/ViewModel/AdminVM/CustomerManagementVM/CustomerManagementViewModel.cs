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
        private CustomerDTO _selectedItem;

        public CustomerDTO SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value;  OnPropertyChanged(); }
        }

        //Add page
        private string _editname;

        public string EditName
        {
            get { return _editname; }
            set { _editname = value; }
        }
        private string _editemail ;

        public string EditEmail
        {
            get { return _editemail ; }
            set { _editemail  = value; }
        }
        private string _editphoneNumber;

        public string EditPhoneNumber
        {
            get { return _editphoneNumber; }
            set { _editphoneNumber = value; }
        }
        private string _editspend;

        public string EditSpend
        {
            get { return _editspend; }
            set { _editspend = value; }
        }
        private string _editdescription;

        public string EditDescription
        {
            get { return _editdescription; }
            set { _editdescription = value; }
        }
        //Edit page
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
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
        public ICommand OpenEditWdCM { get; set; }
        public ICommand EditCusListCM { get; set; }
        public ICommand DeleteCusListCM { get; set; }
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
            OpenEditWdCM = new RelayCommand<object>((p) => { return true; }, (p) => {
                EditName = SelectedItem.DisplayName;
                EditEmail = SelectedItem.Email;
                EditDescription = SelectedItem.Description;
                EditPhoneNumber = SelectedItem.PhoneNumber;
                EditSpend = SelectedItem.Spend.ToString();
                EditCustomerWindow wd = new EditCustomerWindow();
                wd.ShowDialog();
            });
            EditCusListCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                Customer newCus = new Customer {
                    ID = SelectedItem.ID,
                    Description = EditDescription,
                    PhoneNumber = EditPhoneNumber,
                    Email = EditEmail,
                    DisplayName = EditName,
                    Spend= decimal.Parse(EditSpend),
                    IsDeleted = false,
                };
                (bool success, string messageEdit) = await CustomerService.Ins.EditCusList(newCus, SelectedItem.ID);
                if (success)
                {
                    p.Close();
                    CustomerList = new ObservableCollection<CustomerDTO>(await CustomerService.Ins.GetAllCus());
                }
                else
                {
                    MessageBox.Show(messageEdit);
                }
            });
            DeleteCusListCM = new RelayCommand<object>((p) => { return true; }, async (p) => 
            { 
                (bool sucess, string messageDelete) = await CustomerService.Ins.DeleteCustomer(SelectedItem.ID);
                if (sucess)
                {
                    CustomerList.Remove(SelectedItem);
                }
                else
                {
                    MessageBox.Show(messageDelete);
                }
            });
        }
    }
}
