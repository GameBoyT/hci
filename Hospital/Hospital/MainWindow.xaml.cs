using System;
using System.Collections.Generic;
using Model;
using System.Windows;

namespace Hospital
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void roomOptions(object sender, RoutedEventArgs e)
        {
            RoomOptions room = new RoomOptions();
            room.Show();
        }
    }
}
