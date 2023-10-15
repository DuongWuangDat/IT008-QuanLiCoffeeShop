using QuanLiCoffeeShop.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace QuanLiCoffeeShop.View.Admin.StaffManagemet
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
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
