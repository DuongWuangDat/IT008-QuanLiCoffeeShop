using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Data;

namespace QuanLiCoffeeShop.Converter
{
    public class PriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal price)
            {
                string valuePrice;
                string a = value.ToString();
                valuePrice = a.Substring(0, a.Length - 5);
                int b = int.Parse(valuePrice);
                string c = b.ToString("N");               
                valuePrice = c.Substring(0, c.Length - 3);
                return valuePrice;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
