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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Game_UI.Views
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void LaunchGame()
        {
            Hide();
            new MainWindow().ShowDialog();
            Close();
        }

        private void LaunchGame(object sender, RoutedEventArgs e) => LaunchGame();

        private void LaunchGame(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) LaunchGame();
        }
    }
}
