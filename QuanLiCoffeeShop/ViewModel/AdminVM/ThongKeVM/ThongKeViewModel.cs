using QuanLiCoffeeShop.View.Admin;
using System;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows;
using QuanLiCoffeeShop.DTOs;
using QuanLiCoffeeShop.View.Admin.ThongKe.LichSuBan;
using QuanLiCoffeeShop.View.Admin.ThongKe.DoanhThu;
using QuanLiCoffeeShop.View.Admin.ThongKe.MonUaThich;
using QuanLiCoffeeShop.ViewModel.AdminVM.ThongKeVM;
using QuanLiCoffeeShop.View.Admin.ThongKe;
using QuanLiCoffeeShop.Model;
using QuanLiCoffeeShop.Model.Service;
using QuanLiCoffeeShop.View.MessageBox;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using LiveCharts.Wpf;
using LiveCharts;

namespace QuanLiCoffeeShop.ViewModel.AdminVM.ThongKeVM
{
    public partial class ThongKeViewModel : BaseViewModel
    {

        public ICommand HistoryCM { get; set; }
        public ICommand RevenueCM { get; set; }
        public ICommand FavorCM { get; set; }   

        public ThongKeViewModel()
        {
            HistoryCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new LichSuTable();
            });

            #region DoanhThu
            RevenueCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new DoanhThuTable();
            });

            var dates = RevenueDataList.Select(data => data.Date).ToArray();
            var revenueValues = RevenueDataList.Select(data => data.Revenue).ToArray();

            RevenueSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Revenue",
                    Values = new ChartValues<double>(revenueValues),
                }
            };
            
            Labels = dates;
            YFormatter = value => value.ToString("C");
            #endregion
            
            FavorCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MonUaThichTable();
            });
        }
    }
}
