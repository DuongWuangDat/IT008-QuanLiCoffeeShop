using QuanLiCoffeeShop.Model;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;

namespace QuanLiCoffeeShop.ViewModel
{


    public class StaffViewModel : BaseViewModel
    {
        public ObservableCollection<Staff> Employees { get; set; }

        public StaffViewModel()
        {
            Employees = new ObservableCollection<Staff>();

        }
    }
}
