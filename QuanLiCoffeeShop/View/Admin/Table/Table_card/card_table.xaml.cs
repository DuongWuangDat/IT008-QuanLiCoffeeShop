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

<<<<<<<< HEAD:QuanLiCoffeeShop/View/Admin/StaffManagement/AddStaff.xaml.cs
namespace QuanLiCoffeeShop.View.Admin.StaffManagement
{
    /// <summary>
    /// Interaction logic for AddStaff.xaml
    /// </summary>
    public partial class AddStaff : Window
    {
        public AddStaff()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
========
namespace QuanLiCoffeeShop.View.Admin.Table.Table_card
{
    /// <summary>
    /// Interaction logic for card_table.xaml
    /// </summary>
    public partial class card_table : UserControl
    {
        public card_table()
        {
            InitializeComponent();
        }
    }
}
