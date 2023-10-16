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

    public class CountToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int count)
            {
                return $"Nhân viên ({count})";
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

}
