using QuanLiCoffeeShop.DTOs;
using QuanLiCoffeeShop.Model;
using QuanLiCoffeeShop.Model.Service;
using QuanLiCoffeeShop.Utils;
using QuanLiCoffeeShop.View.Staff.Sales;
using QuanLiCoffeeShop.View.MessageBox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Microsoft.Win32;

namespace QuanLiCoffeeShop.ViewModel.StaffVM.SalesVM
{
    internal class SalesMainPageViewModel:BaseViewModel
    {
        public ICommand LoadSeatPageCM {  get; set; }
        public ICommand LoadProductPageCM { get; set; }
        public ICommand FirstLoadCM { get; set; }
        public SalesMainPageViewModel() {
            FirstLoadCM = new RelayCommand<Frame>((p) => { return true; }, (p) => {
                p.Content = new SeatPage();
            });
            LoadSeatPageCM = new RelayCommand<Frame>((p)=> { return true; }, (p)=> {
                p.Content = new SeatPage();
            });
            LoadProductPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) => {
                p.Content = new ProductPage();
            });
        }
    }
}
