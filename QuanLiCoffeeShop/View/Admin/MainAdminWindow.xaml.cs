﻿using QuanLiCoffeeShop.ViewModel.AdminVM;
using System.Windows;
using System.Windows.Input;
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
    }
}
