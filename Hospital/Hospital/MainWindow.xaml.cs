using System.Windows;
using Service;
using System;
using Model;
using System.Collections.Generic;

namespace Hospital
{
    public partial class MainWindow : Window
    {
        private readonly RenovationService _renovationService = new RenovationService();
        private readonly RoomService _roomService = new RoomService();
        public MainWindow()
        {
            InitializeComponent();               
        }

        private void Button_Click_Patient(object sender, RoutedEventArgs e)
        {
            var new_window = new PatientWindow();
            new_window.Show();
            //this.Close();
        }

        private void Button_Click_Doctor(object sender, RoutedEventArgs e)
        {
            View.Doctor.DoctorWindow doctorWindow = new View.Doctor.DoctorWindow();
            doctorWindow.Show();
            //this.Close();
        }

        private void Button_Click_Director(object sender, RoutedEventArgs e)
        {
            View.Director.DirectorWindow directorWindow = new View.Director.DirectorWindow();
            directorWindow.Show();
            //this.Close();
        }

        private void Button_Click_Secretary(object sender, RoutedEventArgs e)
        {
            SecretaryFun secretaryFun = new SecretaryFun();
            secretaryFun.Show();
            //this.Close();
        }
    }
}
