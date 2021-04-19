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
            updateButton1.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = Visibility.Collapsed;
        }

        private void WindowUpdate()
        {
            patientsToShow = patientController.GetAll();
            secretaryDataGrid.ItemsSource = patientsToShow;
        }

        private void ClearFileds()
        {
            JMBGTextBox.Clear();
            usernameTextBox.Clear();
            nameTextBox.Clear();
            surnameTextBox.Clear();
            passwordTextBox.Clear();
            addressTextBox.Clear();
            emailTextBox.Clear();
            datePickerPatient.SelectedDate = null;

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

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Patient patient = (Patient)secretaryDataGrid.SelectedItems[0];
                JMBGTextBox.Text = patient.User.Jmbg;
                usernameTextBox.Text = patient.User.Username;
                nameTextBox.Text = patient.User.FirstName;
                surnameTextBox.Text = patient.User.LastName;
                passwordTextBox.Text = patient.User.Password;
                addressTextBox.Text = patient.User.Address;
                emailTextBox.Text = patient.User.Email;
                datePickerPatient.SelectedDate = patient.User.DateOfBirth;
                updateButton1.Visibility = Visibility.Visible;
                cancelButton.Visibility = Visibility.Visible;
            }
            catch
            {
                MessageBox.Show("Select the patient", "greska");
            }
        }

        private void updateButton1_Click(object sender, RoutedEventArgs e)
        {
            Patient oldPatient = (Patient)secretaryDataGrid.SelectedItems[0];
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
            Patient patient1 = new Patient(patient);
            User newPatient = new User(jmbg,
                                             firstName,
                                             lastName,
                                             username,
                                             password,
                                             email,
                                             address,
                                             dateBirth);
            Patient patient2 = new Patient(newPatient);
            patientController.Update(patient2);
            updateButton1.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = Visibility.Collapsed;
            ClearFileds();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            updateButton1.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = Visibility.Collapsed;
            ClearFileds();

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
            WindowUpdate();
            ClearFileds();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Patient patient = (Patient)secretaryDataGrid.SelectedItems[0];
                patientController.Delete(patient.User.Jmbg);
                WindowUpdate();
            }
            catch
            {
                MessageBox.Show("You have to select an appointment to delete!");
            }
        }

    }
}
