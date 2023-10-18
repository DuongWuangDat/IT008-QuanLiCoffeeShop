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
        List<problem> pr = new List<problem>();
        public Page_main()
        {
            InitializeComponent();
            pr.Add(new problem() { STT = "1", Name = "Hư bàn", Status = "chưa sửa",Node="Đạt Fa đập" });
            pr.Add(new problem() { STT = "2", Name = "Hư bàn", Status = "chưa sửa", Node = "Đạt Fa đập" });
            pr.Add(new problem() { STT = "3", Name = "Hư bàn", Status = "chưa sửa", Node = "Đạt Fa đập" });
            pr.Add(new problem() { STT = "2", Name = "Hư bàn", Status = "chưa sửa", Node = "Đạt Fa đập" });
            pr.Add(new problem() { STT = "2", Name = "Hư bàn", Status = "chưa sửa", Node = "Đạt Fa đập" });
            pr.Add(new problem() { STT = "2", Name = "Hư bàn", Status = "chưa sửa", Node = "Đạt Fa đập" });
            pr.Add(new problem() { STT = "2", Name = "Hư bàn", Status = "chưa sửa", Node = "Đạt Fa đập" });
            pr.Add(new problem() { STT = "2", Name = "Hư bàn", Status = "chưa sửa", Node = "Đạt Fa đập" });
            pr.Add(new problem() { STT = "2", Name = "Hư bàn", Status = "chưa sửa", Node = "Đạt Fa đập" });
            pr.Add(new problem() { STT = "2", Name = "Hư bàn", Status = "chưa sửa", Node = "Đạt Fa đập" });
            pr.Add(new problem() { STT = "2", Name = "Hư bàn", Status = "chưa sửa", Node = "Đạt Fa đập" });
            pr.Add(new problem() { STT = "2", Name = "Hư bàn", Status = "chưa sửa", Node = "Đạt Fa đập" });
            pr.Add(new problem() { STT = "5", Name = "Hư bàn", Status = "chưa sửa", Node = "Đạt Fa đập" });
            pr.Add(new problem() { STT = "6", Name = "Hư bàn", Status = "chưa sửa", Node = "Đạt Fa đập" });
            pr.Add(new problem() { STT = "7", Name = "Hư bàn", Status = "chưa sửa", Node = "Đạt Fa đập" });
            pr.Add(new problem() { STT = "8", Name = "Hư bàn", Status = "chưa sửa", Node = "Đạt Fa đập" });
            prb_list.ItemsSource = pr;
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
