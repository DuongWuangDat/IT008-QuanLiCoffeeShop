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

namespace QuanLiCoffeeShop.View.Admin.Problem.Problem_page_main
{
    /// <summary>
    /// Interaction logic for Page_main.xaml
    /// </summary>
    
    public partial class Page_main : Page
    {
       
        public Page_main()
        {
            InitializeComponent();
           
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == textBox.Tag.ToString())
            {
                textBox.Text = "";
            }
        }
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = textBox.Tag.ToString();

            }
        }
    }
    class problem
    {
        public string STT { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Node { get; set; }
    }
}
