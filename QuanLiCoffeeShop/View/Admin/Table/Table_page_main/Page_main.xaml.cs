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

namespace QuanLiCoffeeShop.View.Admin.Table.Table_page_main
{
    /// <summary>
    /// Interaction logic for Page_main.xaml
    /// </summary>
    public partial class Page_main : Page
    {
        List <table> ta=new List <table> ();
        public Page_main()
        {
            InitializeComponent();
            ta.Add(new table() { Name="bàn 1", Status="còn trống"});
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });
            ta.Add(new table() { Name = "bàn 1", Status = "còn trống" });

            list_table.ItemsSource = ta;
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
        class table
        {
            public string Name { get; set; }
            public string Status { get; set; }
        }

    }
}
