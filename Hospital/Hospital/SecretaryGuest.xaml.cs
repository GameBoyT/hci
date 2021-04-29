using Controller;
using Model;
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
    /// Interaction logic for SecretaryGuest.xaml
    /// </summary>
    public partial class SecretaryGuest : Window
    {
        private PatientController patientController = new PatientController();


        public SecretaryGuest()
        {
            InitializeComponent();
        }

        private Patient CreatePatientFromData()
        {
            string jmbg = JMBGTB.Text;
            string firstName = "";
            string lastName = "";
            string username = userNameTB.Text;
            string password = passwordTB.Text;
            string email = "";
            string address = "";
            DateTime dateBirth = new DateTime(2021, 1, 1);
            User patient = new User(jmbg, firstName, lastName, username, password, email, address, dateBirth);
            return new Patient(patient);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = CreatePatientFromData();
            patientController.Save(patient);
        }
    }
}
