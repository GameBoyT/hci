using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hospital.View.Director
{

    public partial class DirectorWindow : Window
    {
        public DirectorWindow()
        {
            InitializeComponent();
        }

        private void View_rooms(object sender, RoutedEventArgs e)
        {
            RoomCrud roomWindow = new RoomCrud();
            roomWindow.Show();
            System.Windows.Application.Current.MainWindow.Hide();
        }

        private void ViewEquipments(object sender, RoutedEventArgs e)
        {
            Equipment equipment = new Equipment();
            equipment.Show();
            Application.Current.MainWindow.Hide();
        }

        private void ViewMedicine(object sender, RoutedEventArgs e)
        {
            MedicineCrud meds = new MedicineCrud();
            meds.Show();
            System.Windows.Application.Current.MainWindow.Hide();

        }

    }
}
