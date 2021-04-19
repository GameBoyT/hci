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

namespace Hospital
{
    /// <summary>
    /// Interaction logic for SecretaryFun.xaml
    /// </summary>
    public partial class SecretaryFun : Window
    {
        public SecretaryFun()
        {
            InitializeComponent();
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click_PatientCRUD(object sender, RoutedEventArgs e)
        {
            SecretaryPatientCRUD secretaryPatientCRUD = new SecretaryPatientCRUD();
            secretaryPatientCRUD.Show();
            this.Close();
        }

        private void CRUD_Appointment_Click(object sender, RoutedEventArgs e)
        {
            SecretaryAppointmentCRUD secretaryAppointmentCRUD = new SecretaryAppointmentCRUD();
            secretaryAppointmentCRUD.Show();
            this.Close();
        }

        private void Allergens_Click(object sender, RoutedEventArgs e)
        {
            SecretaryAllergens secretaryAllergens = new SecretaryAllergens();
            secretaryAllergens.Show();
            this.Close();
        }
    }
}
