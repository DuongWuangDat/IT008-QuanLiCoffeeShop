using QuanLiCoffeeShop.DTOs;
using QuanLiCoffeeShop.View.Staff.ReportError;
using QuanLiCoffeeShop.View.Staff.Sales;
using QuanLiCoffeeShop.View.Staff.SalesHistory;
using QuanLiCoffeeShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLiCoffeeShop.ViewModel.StaffVM
{
    public class MainStaffViewModel : BaseViewModel
    {
        public static StaffDTO curentstaff;
        private string _currentName;

        public string currentName
        {
            get { return _currentName; }
            set { _currentName = value; OnPropertyChanged(); }
        }
        public ICommand FirstLoadCM {  get; set; }
        public ICommand LoadBanHangPageCM {  get; set; }
        public ICommand LoadErrorPageCM {  get; set; }
        public ICommand LoadHistoryPageCM {  get; set; }


        public MainStaffViewModel() {
            FirstLoadCM = new RelayCommand<Frame>((p) => { return true; }, (p) => {
                p.Content = new SalesMainPage();
                currentName = curentstaff.DisplayName;
            });
            LoadBanHangPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) => {
                p.Content = new SalesMainPage();                
            });
            LoadErrorPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) => {
                p.Content = new ReportError();
            });
            LoadHistoryPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) => {
                p.Content = new SalesHistory();
            });
        }
    }
}
