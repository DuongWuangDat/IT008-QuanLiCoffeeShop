﻿using QuanLiCoffeeShop.ViewModel.MessageBoxVM;
using System.Windows;
using System.Windows.Input;

namespace QuanLiCoffeeShop.View.MessageBox
{
    /// <summary>
    /// Interaction logic for ConfirmLogOut.xaml
    /// </summary>
    public partial class ConfirmLogOut : Window
    {
        public ConfirmLogOut()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void No_btn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Window.GetWindow(this).Close();
        }

        private void Yes_btn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
