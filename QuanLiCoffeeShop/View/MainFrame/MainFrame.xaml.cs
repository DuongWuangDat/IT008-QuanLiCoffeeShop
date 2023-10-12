﻿using System;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLiCoffeeShop.View.MainFrame
{
    /// <summary>
    /// Interaction logic for MainFrame.xaml
    /// </summary>
    public partial class MainFrame : Window
    {
        public MainFrame()
        {
            InitializeComponent();
        }

        private void Overlay_MouseDown(object sender, MouseButtonEventArgs e)
        {

            BeginStoryboard((Storyboard)Resources["MenuClose"]);
        }
    }
}
