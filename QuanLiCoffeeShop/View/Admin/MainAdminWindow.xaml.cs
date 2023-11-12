using QuanLiCoffeeShop.ViewModel.AdminVM;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace QuanLiCoffeeShop.View.Admin
{
    /// <summary>
    /// Interaction logic for MainAdminWindow.xaml
    /// </summary>
    public partial class MainAdminWindow : Window
    {
        public MainAdminWindow()
        {
            InitializeComponent();
            DataContext = new MainAdminViewModel();
        }

        private void Overlay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BeginStoryboard((Storyboard)Resources["MenuClose"]);
        }

        private void AdminWD_Closed(object sender, System.EventArgs e)
        {
            //  this.Owner.Visibility = Visibility.Visible;
        }

        private void Overlay_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Overlay.Visibility == Visibility.Visible)
            {
                // Ngăn chặn sự kiện chuột từ việc truyền đi
                e.Handled = true;

                // Ẩn Overlay khi click vào bất kỳ vị trí nào khác overlay
                Overlay.Visibility = Visibility.Collapsed;
            }
        }
    }
}
