using QuanLiCoffeeShop.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace QuanLiCoffeeShop.View.Admin.StaffManagement
{
    /// <summary>
    /// Interaction logic for AddStaff.xaml
    /// </summary>
    ///
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window window = sender as Window;
            window.Close();
        }
    }
}
