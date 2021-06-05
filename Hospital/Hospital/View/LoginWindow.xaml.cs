using Hospital.View.Doctor;
using Hospital.ViewModels;
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

namespace Hospital.View
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            DoctorMainWindow doctorMainView = new DoctorMainWindow();
            doctorMainView.Show();
            this.Close();
        }
    }
}
