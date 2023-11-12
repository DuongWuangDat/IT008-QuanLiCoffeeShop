using QuanLiCoffeeShop.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        private void DataGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Lấy hàng được click
            var row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;

            // Nếu là hàng, thì chọn toàn bộ hàng
            if (row != null)
            {
                if (!row.IsSelected)
                {
                    row.IsSelected = true;
                }
                else
                {
                    row.IsSelected = false;
                }

                e.Handled = true; // Ngăn chặn sự kiện click từ việc được truyền xuống các thành phần con của DataGrid
            }
        }
    }
}
