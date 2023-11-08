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
using System.Windows.Shapes;
using Microsoft.Win32;

namespace QuanLiCoffeeShop.View.Admin.SanPham
{
    /// <summary>
    /// Interaction logic for EditSanPham.xaml
    /// </summary>
    public partial class EditSanPham : Window
    {
        public ImageSource ImageSource { get; set; }
        public EditSanPham()
        {
            InitializeComponent();
            DataContext = this;
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
        //private void BoQua_btn_Click(object obj, RoutedEventArgs e)
        //{
        //    Window.GetWindow(this).Close();
        //}
    }
}
