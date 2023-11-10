using QuanLiCoffeeShop.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace QuanLiCoffeeShop.View.Admin.StaffManagement
{
    /// <summary>
    /// Interaction logic for StaffPage.xaml
    /// </summary>
    public partial class StaffPage : Page
    {
        public StaffPage()
        {
            InitializeComponent();
            DataContext = new StaffViewModel();

        }
    }
}
