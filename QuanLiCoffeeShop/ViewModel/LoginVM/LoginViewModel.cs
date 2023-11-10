using QuanLiCoffeeShop.DTOs;
using QuanLiCoffeeShop.Model;
using QuanLiCoffeeShop.View.Admin;
using QuanLiCoffeeShop.View.Staff;
using QuanLiCoffeeShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace QuanLiCoffeeShop.ViewModel.LoginVM
{
    public class LoginViewModel : BaseViewModel
    {
        private string _Username;

        public string Username
        {
            get { return _Username; }
            set { _Username = value;  OnPropertyChanged(); }
        }
        private string _Password;

        public string Password
        {
            get { return _Password; }
            set { _Password = value; OnPropertyChanged(); }
        }


        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public LoginViewModel() {
            LoginCommand = new RelayCommand<Window>((p) => 
            { 
                if(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                {
                    return false;
                }
                return true;
            }, 
            async (p) => { 
                await Login(p); 
            });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { 
                return true;
            }, (p) =>
            {
                Password = p.Password;
            });
        }
        #region methods
        async Task Login(Window p)
        {
            Staff staff = await DataProvider.Ins.DB.Staff.Where(x=> x.UserName == Username && x.PassWord == Password).FirstOrDefaultAsync();
            if(staff != null)
            {
                p.Visibility = Visibility.Collapsed;
                
                if(staff.Role == "Quản lí")
                {
                    MainAdminWindow ad = new MainAdminWindow();
                    ad.Show();
                }
                else
                {
                    MainStaffWindow st = new MainStaffWindow();
                    st.Show();
                }
                
            }
            else
            {
                MessageBox.Show("Dang nhap khong hop le");
            }
            
        }
        #endregion
    }
}
