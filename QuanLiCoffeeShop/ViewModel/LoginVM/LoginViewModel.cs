using QuanLiCoffeeShop.Model;
using QuanLiCoffeeShop.View.Admin;
using QuanLiCoffeeShop.ViewModel;
using System;
using System.Collections.Generic;
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
            (p) => { 
                Login(p); 
            });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { 
                return true;
            }, (p) =>
            {
                Password = p.Password;
            });
        }
        #region methods
        void Login(Window p)
        {
            int accCount = DataProvider.Ins.DB.Staff.Where(x=> x.UserName == Username && x.PassWord == Password).Count();
            if(accCount > 0)
            {
                p.Visibility = Visibility.Collapsed;
                MainAdminWindow ad = new MainAdminWindow();
                ad.Show();
            }
            else
            {
                MessageBox.Show("Dang nhap khong hop le");
            }
            
        }
        #endregion
    }
}
