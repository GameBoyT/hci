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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hospital.ViewModels;

namespace Hospital.View.Doctor
{
    public partial class LoginView : Page
    {
        public LoginView()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            DoctorWindowViewModel vm = new DoctorWindowViewModel(NavigationService);
            DoctorMainView doctorMainView = new DoctorMainView(vm);
            NavigationService.Navigate(doctorMainView);
        }
    }
}
