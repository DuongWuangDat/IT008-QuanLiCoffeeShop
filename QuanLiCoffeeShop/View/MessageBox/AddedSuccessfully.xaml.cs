using QuanLiCoffeeShop.ViewModel.MessageBoxVM;
using System.Windows;
using System.Windows.Input;

namespace QuanLiCoffeeShop.View.MessageBox
{
    /// <summary>
    /// Interaction logic for MessageBox.xaml
    /// </summary>
    public partial class AddedSuccessfully : Window
    {
        MessageBoxViewModel messageBoxViewModel;

        public AddedSuccessfully()
        {
            InitializeComponent();
            messageBoxViewModel = new MessageBoxViewModel();
            DataContext = messageBoxViewModel;
        }

        public void SetText(string text)
        {
            messageBoxViewModel.Text = text;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Ok_btn_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }
    }
}
