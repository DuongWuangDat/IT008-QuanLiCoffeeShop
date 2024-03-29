﻿using QuanLiCoffeeShop.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace QuanLiCoffeeShop.Converter
{
    public class CusNameConverter : IValueConverter
    {
        public object Convert(object value, Type TargetType, object parameter, CultureInfo culture)
        {
            if (value == null || ((Customer)value).DisplayName==null
                )
            {
                string st = "Khách vãng lai";
                return st;
            }
            else
            {
                
                return ((Customer)value).DisplayName;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
