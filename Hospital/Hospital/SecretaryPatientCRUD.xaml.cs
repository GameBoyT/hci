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
    /// Interaction logic for SecretaryPatientCRUD.xaml
    /// </summary>
    public partial class SecretaryPatientCRUD : Window
    {

        private PatientController patientController = new PatientController();
        List<Patient> patients = new List<Patient>();
        List<Patient> patientsToShow = new List<Patient>();
            

        public SecretaryPatientCRUD()
        {
            InitializeComponent();
            patientsToShow = patientController.GetAll();
            secretaryDataGrid.ItemsSource = patientsToShow;
        }

        private void DataGrid(object sender, SelectionChangedEventArgs e)
        {
            //patientsToShow = patientController.GetAll();
           // secretaryDataGrid.ItemsSource = patientsToShow;
        }

        private Patient CreatePatientFromData()
        {
            string jmbg = JMBGTextBox.Text;
            string firstName = nameTextBox.Text;
            string lastName = surnameTextBox.Text;
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;
            string email = emailTextBox.Text;
            string address = addressTextBox.Text;
            DateTime dateOfBirth = datePickerPatient.SelectedDate.Value;
            DateTime dateBirth = new DateTime(dateOfBirth.Year, dateOfBirth.Month, dateOfBirth.Day);
            User patient = new User(jmbg, firstName, lastName, username, password, email, address, dateBirth);
            return new Patient(patient);
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            SecretaryFun secretaryFun = new SecretaryFun();
            secretaryFun.Show();
            this.Close();
        }

        private void Button_Click_Create(object sender, RoutedEventArgs e)
        {
            Patient patient = CreatePatientFromData();
            patientController.Save(patient);
        }
    }
}
