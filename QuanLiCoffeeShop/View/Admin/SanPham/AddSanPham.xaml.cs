using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace QuanLiCoffeeShop.View.Admin.SanPham
{
    /// <summary>
    /// Interaction logic for AddSanPham.xaml
    /// </summary>
    public partial class AddSanPham : Window
    {
        public ImageSource ImageSource { get; set; }
        public AddSanPham()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void AddImage_btn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.png;*.jpeg;*.gif|All Files|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                ImageSource = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
    }
}
