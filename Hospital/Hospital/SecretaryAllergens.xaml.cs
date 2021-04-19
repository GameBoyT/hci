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
    /// Interaction logic for SecretaryAllergens.xaml
    /// </summary>
    public partial class SecretaryAllergens : Window
    {
        List<Patient> patients = new List<Patient>();
        private PatientController patientController = new PatientController();

        public SecretaryAllergens()
        {
            InitializeComponent();
            patients = patientController.GetAll();
            secretaryAllergensDataGrid.ItemsSource = patients;
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            /*AllergensDataGrid.AutoGenerateColumns = true;
            Patient patient = (Patient)secretaryAllergensDataGrid.SelectedItems[0];
            AllergensDataGrid.ItemsSource = patient.MedicalRecord.Alergies;*/

        }
    }
}
